using System;
using System.Collections.Generic;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using Pokatun.Core.ViewModels;
using Pokatun.iOS.Controls;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views
{
    public abstract class BaseViewController<TViewModel> : MvxViewController<TViewModel>
        where TViewModel : BaseViewModel
    {
        protected static readonly TitleView ViewTitle = TitleView.Create();

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

            NavigationItem.BackButtonTitle = string.Empty;
            
            NavigationItem.TitleView = ViewTitle;

            string title = ViewModel.Title;
            bool titleNotExists = string.IsNullOrWhiteSpace(title);

            ViewTitle.IsLogoHidden = !titleNotExists;
            ViewTitle.IsTitleHidden = titleNotExists;
            ViewTitle.IsSubtitleHidden = true;
            ViewTitle.Title = title;

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
