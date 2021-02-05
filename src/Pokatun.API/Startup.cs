using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Pokatun.API.Entities;
using Pokatun.API.Helpers;
using Pokatun.API.Models;
using Pokatun.API.Services;
using Pokatun.Data;
using Exceptionless;

namespace Pokatun.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddExceptionless("Wpst0D5qIweH4HGZY34gu0R3f9z6E6YEZvkRmxAP");

            services.AddDbContext<PokatunContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // configure strongly typed settings objects
            IConfigurationSection appSettingsSection = Configuration.GetSection(nameof(AppSettings));
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            AppSettings appSettings = appSettingsSection.Get<AppSettings>();
            byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.Configure<MailSettings>(Configuration.GetSection(nameof(MailSettings)));

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        PokatunContext dbContext = context.HttpContext.RequestServices.GetRequiredService<PokatunContext>();
                        long id = long.Parse(context.Principal.Identity.Name);

                        if (!dbContext.Hotels.Any(x => x.Id == id) && !dbContext.Tourists.Any(x => x.Id == id))
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringJsonConverter()));

            services.Configure<ApiBehaviorOptions>(a => a.InvalidModelStateResponseFactory = (context) =>
            {
                ServerResponce errorResonce = ServerResponce.ForErrors(context.ModelState.Keys.Select(k => k + "ValidationError"));

                return new BadRequestObjectResult(errorResonce);
            });

            services.AddScoped<IAccountsApiService, AccountsApiService>();
            services.AddScoped<IHotelsApiService, HotelsApiService>();
            services.AddScoped<IHotelNumbersApiService, HotelNumbersApiService>();
            services.AddScoped<IEmailApiService, EmailApiService>();
            services.AddScoped<IFileSystem, FileSystem>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionless();

            UpdateDatabase(app);

            if (string.IsNullOrWhiteSpace(env.WebRootPath))
            {
                env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<PokatunContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
