using System.Net;
using System.Net.Http;
using Acr.UserDialogs;
using FFImageLoading;
using FFImageLoading.Config;
using Microsoft.Extensions.Caching.Memory;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Pokatun.Core.Executors;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Xamarin.Essentials;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            #if DEBUG

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            #endif

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            
            Mvx.IoCProvider.RegisterSingleton(() => UserDialogs.Instance);
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ISecureStorage, SecureStorageImplementation>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IDeviceInfo, DeviceInfoImplementation>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IMediaPicker, MediaPickerImplementation>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<INetworkRequestExecutor, NetworkRequestExecutor>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IAuthExecutor, AuthExecutor>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IMemoryCache>(() => new MemoryCache(new MemoryCacheOptions()));

            #if DEBUG

            Mvx.IoCProvider.RegisterSingleton<IRestClient>(
                () =>
                {
                    RestClient client = new RestClient(string.Format(Constants.BaseUrl,
                        Mvx.IoCProvider.Resolve<IDeviceInfo>().Platform == DevicePlatform.iOS
                        ? Constants.iOSDebugIP
                        : Constants.AndroidDebugIP
                    ));
                    client.UseNewtonsoftJson();

                    return client;
                }
            );

            #else

            Mvx.IoCProvider.RegisterSingleton<IRestClient>(() =>
            {
                RestClient client = new RestClient(Constants.BaseUrl);

                client.UseNewtonsoftJson();

                return client;
            });

            #endif

            ImageService.Instance.Initialize(new Configuration { HttpClient = new HttpClient() });

            RegisterCustomAppStart<AppStart>();
        }
    }
}
