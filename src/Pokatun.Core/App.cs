using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Pokatun.Core.ViewModels.ChoiseUserRole;

namespace Pokatun.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.IoCProvider.RegisterSingleton(() => UserDialogs.Instance);

            RegisterAppStart<ChoiseUserRoleViewModel>();
        }
    }
}
