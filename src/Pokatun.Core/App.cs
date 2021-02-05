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
using Pokatun.Core.Factories;
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
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IHotelFinalSetupExecutor, HotelFinalSetupExecutor>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ITouristFinalSetupExecutor, TouristFinalSetupExecutor>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IMemoryCache>(() => new MemoryCache(new MemoryCacheOptions()));
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IRestClientFactory, RestClientFactory>();

            ImageService.Instance.Initialize(new Configuration { HttpClient = new HttpClient() });

            RegisterCustomAppStart<AppStart>();
        }
    }
}
