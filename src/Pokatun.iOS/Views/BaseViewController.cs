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
        protected virtual IDictionary<UITextField, int> MaxLenght => null;

        private static readonly TitleView TitleView = TitleView.Create();

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
            
            NavigationItem.TitleView = TitleView;

            string title = ViewModel.Title;
            bool titleNotExists = string.IsNullOrWhiteSpace(title);

            TitleView.IsLogoHidden = !titleNotExists;
            TitleView.IsTitleHidden = titleNotExists;
            TitleView.Title = title;

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

        protected bool OnShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
        {
            nint newLength = textField.Text.Length + replacementString.Length - range.Length;

            return newLength <= MaxLenght[textField];
        }
    }
}
