using System;
using System.Collections.Generic;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Registration;
using Pokatun.iOS.Converters;
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
                { _usreouTextField, 8 }
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

            set.Bind(_fullCompanyNameTextField).To(vm => vm.FullCompanyName).OneWayToSource();
            set.Bind(_bankCardOrIbanTextField).To(vm => vm.BankCardOrIban).OneWayToSource();
            set.Bind(_bankNameTextField).To(vm => vm.BankName).OneWayToSource();
            set.Bind(_usreouTextField).To(vm => vm.USREOU).OneWayToSource();
            set.Bind(_createAccountButton).To(vm => vm.СreateAccountCommand);

            set.Bind(_fullCompanyNameTextField.Layer)
                .For(l => l.BorderColor)
                .To(vm => vm.IsFullCompanyNameInvalid)
                .WithConversion<BorderEditTextValidationConverter>()
                .OneWay();

            set.Bind(_bankCardOrIbanTextField.Layer)
                .For(v => v.BorderColor)
                .To(vm => vm.IsBankCardOrIbanInvalid)
                .WithConversion<BorderEditTextValidationConverter>()
                .OneWay();

            set.Bind(_bankNameTextField.Layer)
                .For(l => l.BorderColor)
                .To(vm => vm.IsBankNameInvalid)
                .WithConversion<BorderEditTextValidationConverter>()
                .OneWay();

            set.Bind(_usreouTextField.Layer)
                .For(l => l.BorderColor)
                .To(vm => vm.IsUsreouInvalid)
                .WithConversion<BorderEditTextValidationConverter>()
                .OneWay();

            set.Bind(_fullCompanyNameTextField)
                .For(v => v.TextColor)
                .To(vm => vm.IsFullCompanyNameInvalid)
                .WithConversion<EditTextValidationConverter>()
                .OneWay();

            set.Bind(_bankCardOrIbanTextField)
                .For(v => v.TextColor)
                .To(vm => vm.IsBankCardOrIbanInvalid)
                .WithConversion<EditTextValidationConverter>()
                .OneWay();

            set.Bind(_bankNameTextField)
                .For(l => l.TextColor)
                .To(vm => vm.IsBankNameInvalid)
                .WithConversion<EditTextValidationConverter>()
                .OneWay();

            set.Bind(_usreouTextField)
                .For(l => l.TextColor)
                .To(vm => vm.IsUsreouInvalid)
                .WithConversion<EditTextValidationConverter>()
                .OneWay();

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

