using System;
using System.Collections;
using System.Collections.Generic;
using Foundation;
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
        private Dictionary<UITextField, int> _maxLenght;

        public HotelRegistrationFirstStepViewController() : base(nameof(HotelRegistrationFirstStepViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            _maxLenght = new Dictionary<UITextField, int>
            {
                { _hotelNameEditText, 64 },
                { _phoneNumberEditText, 13 },
                { _emailEditText, 64 },
                { _passwordEditText, 32 },
                { _confirmPasswordEditText, 32 }
            };

            _furtherButton.ApplyBigButtonStyle();

            _hotelNameEditText.ApplyBorderedEditTextStyle();
            _phoneNumberEditText.ApplyBorderedEditTextStyle();
            _emailEditText.ApplyBorderedEditTextStyle();
            _passwordEditText.ApplyBorderedEditTextStyle();
            _confirmPasswordEditText.ApplyBorderedEditTextStyle();

            _hotelNameEditText.ShouldChangeCharacters += OnShouldChangeCharacters;
            _phoneNumberEditText.ShouldChangeCharacters += OnShouldChangeCharacters;
            _emailEditText.ShouldChangeCharacters += OnShouldChangeCharacters;
            _passwordEditText.ShouldChangeCharacters += OnShouldChangeCharacters;
            _confirmPasswordEditText.ShouldChangeCharacters += OnShouldChangeCharacters;

            _hotelNameEditText.Placeholder = Strings.Name;
            _phoneNumberEditText.Placeholder = Strings.Phone;
            _emailEditText.Placeholder = Strings.Email;
            _passwordEditText.Placeholder = Strings.Password;
            _confirmPasswordEditText.Placeholder = Strings.ConfirmPassword;
            _furtherButton.SetTitle(Strings.Further, UIControlState.Normal);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(_hotelNameEditText).To(vm => vm.HotelName).OneWayToSource();
            set.Bind(_phoneNumberEditText).To(vm => vm.PhoneNumber).OneWayToSource();
            set.Bind(_emailEditText).To(vm => vm.Email).OneWayToSource();
            set.Bind(_passwordEditText).To(vm => vm.Password).OneWayToSource();
            set.Bind(_confirmPasswordEditText).To(vm => vm.PasswordConfirm).OneWayToSource();
            set.Bind(_furtherButton).To(vm => vm.FurtherCommand);

            set.Apply();
        }

        private bool OnShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
        {
            nint newLength = textField.Text.Length + replacementString.Length - range.Length;

            return newLength <= _maxLenght[textField];
        }

        public override void ViewDidDisappear(bool animated)
        {
            _hotelNameEditText.ResetStyles();
            _phoneNumberEditText.ResetStyles();
            _emailEditText.ResetStyles();
            _passwordEditText.ResetStyles();
            _confirmPasswordEditText.ResetStyles();

            _hotelNameEditText.ShouldChangeCharacters -= OnShouldChangeCharacters;
            _phoneNumberEditText.ShouldChangeCharacters -= OnShouldChangeCharacters;
            _emailEditText.ShouldChangeCharacters -= OnShouldChangeCharacters;
            _passwordEditText.ShouldChangeCharacters -= OnShouldChangeCharacters;
            _confirmPasswordEditText.ShouldChangeCharacters -= OnShouldChangeCharacters;

            base.ViewDidDisappear(animated);
        }
    }
}

