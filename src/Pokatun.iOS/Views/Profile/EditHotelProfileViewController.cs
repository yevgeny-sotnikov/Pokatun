using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Profile;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.Profile
{
    [MvxModalPresentation(WrapInNavigationController = true, ModalPresentationStyle = UIModalPresentationStyle.BlurOverFullScreen)]
    public partial class EditHotelProfileViewController : BaseViewController<EditHotelProfileViewModel>
    {
        public EditHotelProfileViewController() : base(nameof(EditHotelProfileViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            UIBarButtonItem rightBarButtonItem = new UIBarButtonItem();
            rightBarButtonItem.Image = UIImage.FromBundle("close");

            NavigationItem.SetRightBarButtonItem(rightBarButtonItem, false);

            _saveChangesButton.ApplyBigButtonStyle();

            _hotelNameEditText.ApplyBorderedEditTextStyle();
            _fullCompanyNameTextField.ApplyBorderedEditTextStyle();

            _hotelNameEditText.KeyboardType = UIKeyboardType.Default;
            _fullCompanyNameTextField.KeyboardType = UIKeyboardType.Default;

            _hotelNameEditText.Placeholder = Strings.Name;
            _fullCompanyNameTextField.Placeholder = Strings.FullCompanyName;

            _personalDataTab.Text = Strings.PersonalData;
            _hotelInfoTab.Text = Strings.HotelInfo;
            _saveChangesButton.SetTitle(Strings.SaveChanges, UIControlState.Normal);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(NavigationItem.RightBarButtonItem).To(vm => vm.CloseCommand);
            set.Bind(_hotelNameEditText).For(v => v.Text).To(vm => vm.HotelName).OneWayToSource();
            set.Bind(_fullCompanyNameTextField).For(v => v.Text).To(vm => vm.FullCompanyName).OneWayToSource();

            set.Bind(_hotelNameEditText).For(v => v.Highlighted).To(vm => vm.IsHotelNameInvalid).OneWay();
            set.Bind(_fullCompanyNameTextField).For(v => v.Highlighted).To(vm => vm.IsFullCompanyNameInvalid).OneWay();

            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

