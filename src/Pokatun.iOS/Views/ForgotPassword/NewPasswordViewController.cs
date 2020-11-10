using System;
using System.Collections.Generic;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.ForgotPassword;
using Pokatun.iOS.Converters;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.ForgotPassword
{
    [MvxChildPresentation]
    public partial class NewPasswordViewController : BaseViewController<NewPasswordViewModel>
    {
        private IDictionary<UITextField, int> _maxLenght;
        protected override IDictionary<UITextField, int> MaxLenght => _maxLenght;

        public NewPasswordViewController() : base(nameof(NewPasswordViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            _maxLenght = new Dictionary<UITextField, int>
            {
                { _passwordEditText, 32 },
                { _confirmPasswordEditText, 32 }
            };

            _passwordEditText.ApplyBorderedEditTextStyle();
            _confirmPasswordEditText.ApplyBorderedEditTextStyle();

            _saveButton.ApplyBigButtonStyle();

            _saveButton.TouchUpInside += OnButtonTouched;
            _passwordEditText.ShouldChangeCharacters += OnShouldChangeCharacters;
            _confirmPasswordEditText.ShouldChangeCharacters += OnShouldChangeCharacters;

            _passwordEditText.Placeholder = Strings.Password;
            _confirmPasswordEditText.Placeholder = Strings.ConfirmPassword;
            _saveButton.SetTitle(Strings.SavePassword, UIControlState.Normal);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(_passwordEditText).To(vm => vm.Password).OneWayToSource();
            set.Bind(_confirmPasswordEditText).To(vm => vm.PasswordConfirm).OneWayToSource();
            set.Bind(_saveButton).To(vm => vm.SavePasswordCommand);

            set.Bind(_passwordEditText.Layer)
                .For(l => l.BorderColor)
                .To(vm => vm.IsPasswordInvalid)
                .WithConversion<BorderEditTextValidationConverter>()
                .OneWay();

            set.Bind(_confirmPasswordEditText.Layer)
                .For(l => l.BorderColor)
                .To(vm => vm.IsPasswordConfirmInvalid)
                .WithConversion<BorderEditTextValidationConverter>()
                .OneWay();

            set.Bind(_passwordEditText)
                .For(l => l.TextColor)
                .To(vm => vm.IsPasswordInvalid)
                .WithConversion<EditTextValidationConverter>()
                .OneWay();

            set.Bind(_confirmPasswordEditText)
                .For(l => l.TextColor)
                .To(vm => vm.IsPasswordConfirmInvalid)
                .WithConversion<EditTextValidationConverter>()
                .OneWay();

            set.Apply();
        }

        public override void ViewDidDisappear(bool animated)
        {
            _passwordEditText.ResetStyles();
            _confirmPasswordEditText.ResetStyles();

            _saveButton.TouchUpInside -= OnButtonTouched;
            _passwordEditText.ShouldChangeCharacters -= OnShouldChangeCharacters;
            _confirmPasswordEditText.ShouldChangeCharacters -= OnShouldChangeCharacters;

            base.ViewDidDisappear(animated);
        }

        private void OnButtonTouched(object sender, EventArgs e)
        {
            View.EndEditing(true);
        }
    }
}

