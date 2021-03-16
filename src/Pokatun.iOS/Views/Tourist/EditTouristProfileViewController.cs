using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Tourist;
using Pokatun.iOS.Controls;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.Tourist
{
    [MvxModalPresentation(WrapInNavigationController = true, ModalPresentationStyle = UIModalPresentationStyle.BlurOverFullScreen)]
    public partial class EditTouristProfileViewController : BaseViewController<EditTouristProfileViewModel> 
    {
        private TitlePhotoView _titlePhotoView;

        public EditTouristProfileViewController() : base(nameof(EditTouristProfileViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            UIBarButtonItem rightBarButtonItem = new UIBarButtonItem { Image = UIImage.FromBundle("close") };

            _titlePhotoView = TitlePhotoView.Create(true);

            NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(_titlePhotoView), true);
            NavigationItem.SetRightBarButtonItem(rightBarButtonItem, false);

            _saveChangesButton.ApplyBigButtonStyle();

            _fullnameTextField.ApplyBorderedEditTextStyle();
            _phoneTextField.ApplyBorderedEditTextStyle();
            _emailTextField.ApplyBorderedEditTextStyle();

            _emailTextField.Placeholder = Strings.Email;
            _phoneTextField.Placeholder = Strings.Phone;
            _fullnameTextField.Placeholder = Strings.FullName;
            _saveChangesButton.SetTitle(Strings.SaveChanges, UIControlState.Normal);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(NavigationItem.RightBarButtonItem).To(vm => vm.CloseCommand).OneTime();
            set.Bind(_titlePhotoView).For(v => v.ImageStream).To(vm => vm.PhotoStream).OneWay();
            set.Bind(_titlePhotoView).For(nameof(TitlePhotoView.Clicked)).To(vm => vm.AddPhotoCommand).OneTime();

            set.Bind(_emailTextField).For(v => v.Text).To(vm => vm.Email).TwoWay();
            set.Bind(_phoneTextField).For(v => v.Text).To(vm => vm.Phone).TwoWay();
            set.Bind(_fullnameTextField).For(v => v.Text).To(vm => vm.FullName).TwoWay();

            set.Bind(_emailTextField).For(v => v.Highlighted).To(vm => vm.IsEmailInvalid).OneWay();
            set.Bind(_phoneTextField).For(v => v.Highlighted).To(vm => vm.IsPhoneInvalid).OneWay();
            set.Bind(_fullnameTextField).For(v => v.Highlighted).To(vm => vm.IsFullnameInvalid).OneWay();

            set.Bind(_saveChangesButton).To(vm => vm.SaveChangesCommand).OneTime();

            set.Apply();
        }
    }
}

