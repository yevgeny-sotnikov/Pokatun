using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Pokatun.Core.ViewModels.ChoiseUserRole;
using Pokatun.Core.ViewModels.Main;

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

            RegisterAppStart<ChoiseUserRoleViewModel>();
        }
    }
}
