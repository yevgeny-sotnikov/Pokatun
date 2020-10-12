using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.ViewModels.Main;
using UIKit;

namespace Pokatun.iOS.Views.Main
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class MainViewController : BaseViewController<MainViewModel>
    {
        public MainViewController() : base(nameof(MainViewController), null)
        {
        }

    }
}

