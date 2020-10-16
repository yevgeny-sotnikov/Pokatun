using System;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using Pokatun.iOS.Controls;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views
{
    public abstract class BaseViewController<TViewModel> : MvxViewController<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        protected BaseViewController(IntPtr handle)
        {
        }

        protected BaseViewController(string name, NSBundle bundle) : base(name, bundle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
            NavigationController.NavigationBar.Translucent = false;
            NavigationController.NavigationBar.Hidden = false;
            NavigationController.NavigationBar.BarTintColor = ColorPalette.Primary;
            NavigationController.NavigationBar.TintColor = UIColor.White;

            NavigationController.SetNeedsStatusBarAppearanceUpdate();

            NavigationItem.TitleView = TitleView.Create();

            CreateView();

            LayoutView();

            BindView();
        }


        protected virtual void CreateView()
        {
        }

        protected virtual void LayoutView()
        {
        }

        protected virtual void BindView()
        {
        }
    }
}
