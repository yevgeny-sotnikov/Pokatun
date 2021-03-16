using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.ViewModels.Tourist;
using Pokatun.iOS.Controls;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.Tourist
{
    [MvxChildPresentation]
    public partial class ShowTouristProfileViewController : BaseViewController<ShowTouristProfileViewModel>
    {
        private TitlePhotoView _titlePhotoView;

        public ShowTouristProfileViewController() : base(nameof(ShowTouristProfileViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            UIBarButtonItem rightBarButtonItem = new UIBarButtonItem { Image = UIImage.FromBundle("edit") };

            _titlePhotoView = TitlePhotoView.Create();

            NavigationItem.LeftItemsSupplementBackButton = true;
            NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(_titlePhotoView), true);
            NavigationItem.SetRightBarButtonItem(rightBarButtonItem, true);

            _fullnameLabel.ApplyLargeLabelStyle();
            _emailLabel.ApplyLargeLabelStyle();
            _phoneLabel.ApplyLargeLabelStyle();

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(ViewTitle).For(v => v.Title).To(vm => vm.Title).OneWay();
            set.Bind(_titlePhotoView).For(v => v.ImageStream).To(vm => vm.PhotoStream).OneWay();
            set.Bind(_titlePhotoView).For(v => v.Placeholder).To(vm => vm.Placeholder).OneWay();
            set.Bind(NavigationItem.RightBarButtonItem).To(vm => vm.EditCommand).OneTime();

            set.Bind(_fullnameLabel).To(vm => vm.FullName).OneWay();
            set.Bind(_emailLabel).To(vm => vm.Email).OneWay();
            set.Bind(_phoneLabel).To(vm => vm.Phone).OneWay();

            set.Apply();
        }
    }
}

