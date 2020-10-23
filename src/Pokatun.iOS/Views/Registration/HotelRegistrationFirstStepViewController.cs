using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Registration;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.Registration
{
    [MvxChildPresentation]
    public sealed partial class HotelRegistrationFirstStepViewController
        : BaseViewController<HotelRegistrationFirstStepViewModel>
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

            _hotelNameEditText.Placeholder = Strings.Name;
            _phoneNumberEditText.Placeholder = Strings.Phone;
            _emailEditText.Placeholder = Strings.Email;
            _passwordEditText.Placeholder = Strings.Password;
            _confirmPasswordEditText.Placeholder = Strings.ConfirmPassword;
            _furtherButton.SetTitle(Strings.Further, UIControlState.Normal);
        }

        public override void ViewDidDisappear(bool animated)
        {
            _hotelNameEditText.ResetStyles();
            _phoneNumberEditText.ResetStyles();
            _emailEditText.ResetStyles();
            _passwordEditText.ResetStyles();
            _confirmPasswordEditText.ResetStyles();

            base.ViewDidDisappear(animated);
        }
    }
}

