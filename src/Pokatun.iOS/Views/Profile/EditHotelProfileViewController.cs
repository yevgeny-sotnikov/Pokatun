using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Profile;
using Pokatun.iOS.Cells;
using Pokatun.iOS.Controls;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.Profile
{
    [MvxModalPresentation(WrapInNavigationController = true, ModalPresentationStyle = UIModalPresentationStyle.BlurOverFullScreen)]
    public partial class EditHotelProfileViewController : BaseViewController<EditHotelProfileViewModel>
    {
        private MvxSimpleTableViewSource _phonesTableViewSource;

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

            _phonesTableViewSource = new MvxSimpleTableViewSource(_phonesTable, PhoneItemViewCell.Key)
            {
                UseAnimations = true,
                RemoveAnimation = UITableViewRowAnimation.Right,
                AddAnimation = UITableViewRowAnimation.Fade
            };

            _phonesTable.Source = _phonesTableViewSource;

            _saveChangesButton.ApplyBigButtonStyle();

            _hotelNameEditText.ApplyBorderedEditTextStyle();
            _fullCompanyNameTextField.ApplyBorderedEditTextStyle();
            _emailTextField.ApplyBorderedEditTextStyle();

            _addPhoneButton.ApplyBorderedButtonStyle();
            _hotelLocationButton.ApplyBorderedButtonStyle();

            _hotelNameEditText.KeyboardType = UIKeyboardType.Default;
            _fullCompanyNameTextField.KeyboardType = UIKeyboardType.Default;
            _emailTextField.KeyboardType = UIKeyboardType.EmailAddress;

            _hotelNameEditText.Placeholder = Strings.Name;
            _fullCompanyNameTextField.Placeholder = Strings.FullCompanyName;
            _emailTextField.Placeholder = Strings.Email;

            _addPhoneButton.Text = Strings.AddPhone;
            _hotelLocationButton.Text = Strings.HotelLocationAddress;

            _personalDataTab.Text = Strings.PersonalData;
            _hotelInfoTab.Text = Strings.HotelInfo;
            _saveChangesButton.SetTitle(Strings.SaveChanges, UIControlState.Normal);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(NavigationItem.RightBarButtonItem).To(vm => vm.CloseCommand).OneTime();

            set.Bind(_hotelNameEditText).For(v => v.Text).To(vm => vm.HotelName).TwoWay();
            set.Bind(_fullCompanyNameTextField).For(v => v.Text).To(vm => vm.FullCompanyName).TwoWay();
            set.Bind(_emailTextField).For(v => v.Text).To(vm => vm.Email).TwoWay();

            set.Bind(_phonesTableViewSource).To(vm => vm.PhoneNumbers).OneTime();

            set.Bind(_addPhoneButton).For(nameof(BorderedButton.Clicked)).To(vm => vm.AddPhoneCommand).OneTime();

            set.Bind(_hotelNameEditText).For(v => v.Highlighted).To(vm => vm.IsHotelNameInvalid).OneWay();
            set.Bind(_fullCompanyNameTextField).For(v => v.Highlighted).To(vm => vm.IsFullCompanyNameInvalid).OneWay();
            set.Bind(_emailTextField).For(v => v.Highlighted).To(vm => vm.IsEmailInvalid).OneWay();

            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

