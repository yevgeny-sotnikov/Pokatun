using System.Net;
using Acr.UserDialogs;
using FFImageLoading;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Pokatun.Core.Executors;
using Pokatun.Core.ViewModels.ChoiseUserRole;
using RestSharp;
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
            
            #if DEBUG

            Mvx.IoCProvider.RegisterSingleton<IRestClient>(
                () => new RestClient(string.Format(Constants.BaseUrl,
                    Mvx.IoCProvider.Resolve<IDeviceInfo>().Platform == DevicePlatform.iOS
                    ? Constants.iOSDebugIP
                    : Constants.AndroidDebugIP
                )
            ));

            #else

            Mvx.IoCProvider.RegisterSingleton<IRestClient>(() => new RestClient(Constants.BaseUrl));

            #endif

            RegisterCustomAppStart<AppStart>();
        }
    }
}
