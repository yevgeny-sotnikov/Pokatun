using System;
using System.Collections.Generic;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Registration;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.Registration
{
    [MvxChildPresentation]
    public sealed partial class HotelRegistrationSecondStepViewController : BaseViewController<HotelRegistrationSecondStepViewModel>
    {
        private Dictionary<UITextField, int> _maxLenght;
        protected override IDictionary<UITextField, int> MaxLenght => _maxLenght;

        public HotelRegistrationSecondStepViewController() : base(nameof(HotelRegistrationSecondStepViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            _maxLenght = new Dictionary<UITextField, int>
            {
                { _fullCompanyNameTextField, 128 },
                { _bankCardOrIbanTextField, 34 },
                { _bankNameTextField, 64 },
                { _usreouTextField, 12 }
            };

            _createAccountButton.ApplyBigButtonStyle();

            _fullCompanyNameTextField.ApplyBorderedEditTextStyle();
            _bankCardOrIbanTextField.ApplyBorderedEditTextStyle();
            _bankNameTextField.ApplyBorderedEditTextStyle();
            _usreouTextField.ApplyBorderedEditTextStyle();

            _fullCompanyNameTextField.ShouldChangeCharacters += OnShouldChangeCharacters;
            _bankCardOrIbanTextField.ShouldChangeCharacters += OnShouldChangeCharacters;
            _bankNameTextField.ShouldChangeCharacters += OnShouldChangeCharacters;
            _usreouTextField.ShouldChangeCharacters += OnShouldChangeCharacters;

            _fullCompanyNameTextField.Placeholder = Strings.FullCompanyName;
            _bankCardOrIbanTextField.Placeholder = Strings.CardNumberOrIBAN;
            _bankNameTextField.Placeholder = Strings.BankName;
            _usreouTextField.Placeholder = Strings.USREOU;
            _createAccountButton.SetTitle(Strings.CreateAccount, UIControlState.Normal);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            //set.Bind(_hotelNameEditText).To(vm => vm.HotelName).OneWayToSource();
            //set.Bind(_phoneNumberEditText).To(vm => vm.PhoneNumber).OneWayToSource();
            //set.Bind(_emailEditText).To(vm => vm.Email).OneWayToSource();
            //set.Bind(_passwordEditText).To(vm => vm.Password).OneWayToSource();
            //set.Bind(_confirmPasswordEditText).To(vm => vm.PasswordConfirm).OneWayToSource();
            //set.Bind(_furtherButton).To(vm => vm.FurtherCommand);

            //set.Bind(_hotelNameEditText.Layer)
            //    .For(l => l.BorderColor)
            //    .To(vm => vm.IsHotelNameInvalid)
            //    .WithConversion<BorderEditTextValidationConverter>()
            //    .OneWay();

            //set.Bind(_phoneNumberEditText.Layer)
            //    .For(v => v.BorderColor)
            //    .To(vm => vm.IsPhoneNumberInvalid)
            //    .WithConversion<BorderEditTextValidationConverter>()
            //    .OneWay();

            //set.Bind(_emailEditText.Layer)
            //    .For(l => l.BorderColor)
            //    .To(vm => vm.IsEmailInvalid)
            //    .WithConversion<BorderEditTextValidationConverter>()
            //    .OneWay();

            //set.Bind(_passwordEditText.Layer)
            //    .For(l => l.BorderColor)
            //    .To(vm => vm.IsPasswordInvalid)
            //    .WithConversion<BorderEditTextValidationConverter>()
            //    .OneWay();

            //set.Bind(_confirmPasswordEditText.Layer)
            //    .For(l => l.BorderColor)
            //    .To(vm => vm.IsPasswordConfirmInvalid)
            //    .WithConversion<BorderEditTextValidationConverter>()
            //    .OneWay();

            //set.Bind(_hotelNameEditText)
            //    .For(v => v.TextColor)
            //    .To(vm => vm.IsHotelNameInvalid)
            //    .WithConversion<EditTextValidationConverter>()
            //    .OneWay();

            //set.Bind(_phoneNumberEditText)
            //    .For(v => v.TextColor)
            //    .To(vm => vm.IsPhoneNumberInvalid)
            //    .WithConversion<EditTextValidationConverter>()
            //    .OneWay();

            //set.Bind(_emailEditText)
            //    .For(l => l.TextColor)
            //    .To(vm => vm.IsEmailInvalid)
            //    .WithConversion<EditTextValidationConverter>()
            //    .OneWay();

            //set.Bind(_passwordEditText)
            //    .For(l => l.TextColor)
            //    .To(vm => vm.IsPasswordInvalid)
            //    .WithConversion<EditTextValidationConverter>()
            //    .OneWay();

            //set.Bind(_confirmPasswordEditText)
            //    .For(l => l.TextColor)
            //    .To(vm => vm.IsPasswordConfirmInvalid)
            //    .WithConversion<EditTextValidationConverter>()
            //    .OneWay();

            set.Apply();
        }

        public override void ViewDidDisappear(bool animated)
        {
            _fullCompanyNameTextField.ResetStyles();
            _bankCardOrIbanTextField.ResetStyles();
            _bankNameTextField.ResetStyles();
            _usreouTextField.ResetStyles();

            _fullCompanyNameTextField.ShouldChangeCharacters -= OnShouldChangeCharacters;
            _bankCardOrIbanTextField.ShouldChangeCharacters -= OnShouldChangeCharacters;
            _bankNameTextField.ShouldChangeCharacters -= OnShouldChangeCharacters;
            _usreouTextField.ShouldChangeCharacters -= OnShouldChangeCharacters;

            base.ViewDidDisappear(animated);
        }
    }
}

