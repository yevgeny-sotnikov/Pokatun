using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Platforms.Ios.Core;
using Pokatun.Core;
using Pokatun.iOS.Controls;

namespace Pokatun.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            registry.RegisterCustomBindingFactory<MenuItem>(
                nameof(MenuItem.Clicked),
                view => new MvxEventNameTargetBinding<MenuItem>(view, nameof(MenuItem.Clicked))
            );

            base.FillTargetFactories(registry);
        }
    }
}
