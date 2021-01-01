using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Registration;
using Pokatun.iOS.Controls;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.Registration
{
    [MvxChildPresentation]
    public sealed partial class HotelRegistrationFirstStepViewController : BaseViewController<HotelRegistrationFirstStepViewModel>
    {
        public HotelRegistrationFirstStepViewController() : base(nameof(HotelRegistrationFirstStepViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            _furtherButton.ApplyBigButtonStyle();

            _hotelNameEditText.ApplyBorderedEditTextStyle();
            _phoneNumberEditText.ApplyBorderedEditTextStyle();
            _emailEditText.ApplyBorderedEditTextStyle();
            _passwordEditText.ApplyBorderedEditTextStyle();
            _confirmPasswordEditText.ApplyBorderedEditTextStyle();

            _hotelNameEditText.KeyboardType = UIKeyboardType.Default;
            _phoneNumberEditText.KeyboardType = UIKeyboardType.PhonePad;
            _emailEditText.KeyboardType = UIKeyboardType.EmailAddress;
            _passwordEditText.KeyboardType = UIKeyboardType.Default;
            _confirmPasswordEditText.KeyboardType = UIKeyboardType.Default;

            _hotelNameEditText.Placeholder = Strings.Name;
            _phoneNumberEditText.Placeholder = Strings.Phone;
            _emailEditText.Placeholder = Strings.Email;
            _passwordEditText.Placeholder = Strings.Password;
            _confirmPasswordEditText.Placeholder = Strings.ConfirmPassword;
            _furtherButton.SetTitle(Strings.Further, UIControlState.Normal);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(_hotelNameEditText).For(v => v.Text).To(vm => vm.HotelName).OneWayToSource();
            set.Bind(_phoneNumberEditText).For(v => v.Text).To(vm => vm.PhoneNumber).OneWayToSource();
            set.Bind(_emailEditText).For(v => v.Text).To(vm => vm.Email).OneWayToSource();
            set.Bind(_passwordEditText).For(v => v.Text).To(vm => vm.Password).OneWayToSource();
            set.Bind(_confirmPasswordEditText).For(v => v.Text).To(vm => vm.PasswordConfirm).OneWayToSource();
            set.Bind(_furtherButton).To(vm => vm.FurtherCommand);

            set.Bind(_hotelNameEditText).For(v => v.Highlighted).To(vm => vm.IsHotelNameInvalid).OneWay();
            set.Bind(_phoneNumberEditText).For(v => v.Highlighted).To(vm => vm.IsPhoneNumberInvalid).OneWay();
            set.Bind(_emailEditText).For(v => v.Highlighted).To(vm => vm.IsEmailInvalid).OneWay();
            set.Bind(_passwordEditText).For(v => v.Highlighted).To(vm => vm.IsPasswordInvalid).OneWay();
            set.Bind(_confirmPasswordEditText).For(v => v.Highlighted).To(vm => vm.IsPasswordConfirmInvalid).OneWay();

            set.Apply();
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }
    }
}

