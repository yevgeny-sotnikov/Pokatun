using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Registration;
using Pokatun.iOS.Controls;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.Registration
{
    [MvxChildPresentation]
    public sealed partial class HotelRegistrationSecondStepViewController : BaseViewController<HotelRegistrationSecondStepViewModel>
    {
        public HotelRegistrationSecondStepViewController() : base(nameof(HotelRegistrationSecondStepViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            _createAccountButton.ApplyBigButtonStyle();

            _fullCompanyNameTextField.ApplyBorderedEditTextStyle();
            _bankCardOrIbanTextField.ApplyBorderedEditTextStyle();
            _bankNameTextField.ApplyBorderedEditTextStyle();
            _usreouTextField.ApplyBorderedEditTextStyle();

            _fullCompanyNameTextField.KeyboardType = UIKeyboardType.Default;
            _bankCardOrIbanTextField.KeyboardType = UIKeyboardType.NamePhonePad;
            _bankNameTextField.KeyboardType = UIKeyboardType.Default;
            _usreouTextField.KeyboardType = UIKeyboardType.NumberPad;

            _fullCompanyNameTextField.Placeholder = Strings.FullCompanyName;
            _bankCardOrIbanTextField.Placeholder = Strings.CardNumberOrIBAN;
            _bankNameTextField.Placeholder = Strings.BankName;
            _usreouTextField.Placeholder = Strings.USREOU;

            _createAccountButton.SetTitle(Strings.CreateAccount, UIControlState.Normal);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(_fullCompanyNameTextField).For(v => v.Text).To(vm => vm.FullCompanyName).OneWayToSource();
            set.Bind(_bankCardOrIbanTextField).For(v => v.Text).To(vm => vm.BankCardOrIban).OneWayToSource();
            set.Bind(_bankNameTextField).For(v => v.Text).To(vm => vm.BankName).OneWayToSource();
            set.Bind(_usreouTextField).For(v => v.Text).To(vm => vm.USREOU).OneWayToSource();
            set.Bind(_createAccountButton).To(vm => vm.СreateAccountCommand);

            set.Bind(_fullCompanyNameTextField).For(v => v.Highlighted).To(vm => vm.IsFullCompanyNameInvalid).OneWay();
            set.Bind(_bankCardOrIbanTextField).For(v => v.Highlighted).To(vm => vm.IsBankCardOrIbanInvalid).OneWay();
            set.Bind(_bankNameTextField).For(v => v.Highlighted).To(vm => vm.IsBankNameInvalid).OneWay();
            set.Bind(_usreouTextField).For(v => v.Highlighted).To(vm => vm.IsUsreouInvalid).OneWay();

            set.Apply();
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }
    }
}

