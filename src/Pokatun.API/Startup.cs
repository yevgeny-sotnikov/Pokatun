using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Pokatun.API.Models;
using Pokatun.Data;

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
            services.AddDbContext<PokatunContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Constants.AuthOptions.IsUser,

                    ValidateAudience = true,
                    ValidAudience = Constants.AuthOptions.Audience,
                    ValidateLifetime = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constants.AuthOptions.Key)),
                    ValidateIssuerSigningKey = true,
                };
            });

            services.AddControllers();

            services.Configure<ApiBehaviorOptions>(a => a.InvalidModelStateResponseFactory = (context) =>
            {
                ServerResponce errorResonce = ServerResponce.ForErrors(context.ModelState.Keys.Select(k => k + "ValidationError"));

                return new BadRequestObjectResult(errorResonce);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
    }
}
