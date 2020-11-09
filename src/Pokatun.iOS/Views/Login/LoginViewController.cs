using System;
using System.Collections.Generic;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Login;
using Pokatun.iOS.Converters;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.Login
{
    public sealed partial class LoginViewController : BaseViewController<LoginViewModel>
    {
        private IDictionary<UITextField, int> _maxLenght;
        protected override IDictionary<UITextField, int> MaxLenght => _maxLenght;

        public LoginViewController() : base(nameof(LoginViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            _maxLenght = new Dictionary<UITextField, int>
            {
                { _emailTextField, 64 },
                { _passwordTextField, 32 }
            };

            _emailTextField.ApplyBorderedEditTextStyle();
            _passwordTextField.ApplyBorderedEditTextStyle();

            _forgotPasswordButton.Font = Fonts.HelveticaNeueCyrBoldLarge;
            _forgotPasswordButton.TintColor = ColorPalette.PrimaryLight;

            _loginButton.ApplyBigButtonStyle();

            _emailTextField.ShouldChangeCharacters += OnShouldChangeCharacters;
            _passwordTextField.ShouldChangeCharacters += OnShouldChangeCharacters;

            _emailTextField.Placeholder = Strings.Email;
            _passwordTextField.Placeholder = Strings.Password;
            _forgotPasswordButton.SetTitle(Strings.ForgotPasswordQuestion, UIControlState.Normal);
            _loginButton.SetTitle(Strings.ToComeIn, UIControlState.Normal);
            _loginButton.TouchUpInside += OnLoginButtonTouched;
            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(_emailTextField).To(vm => vm.Email).OneWayToSource();
            set.Bind(_passwordTextField).To(vm => vm.Password).OneWayToSource();
            set.Bind(_loginButton).To(vm => vm.LoginCommand);

            set.Bind(_emailTextField.Layer)
                .For(l => l.BorderColor)
                .To(vm => vm.IsEmailInvalid)
                .WithConversion<BorderEditTextValidationConverter>()
                .OneWay();

            set.Bind(_passwordTextField.Layer)
                .For(l => l.BorderColor)
                .To(vm => vm.IsPasswordInvalid)
                .WithConversion<BorderEditTextValidationConverter>()
                .OneWay();

            set.Bind(_emailTextField)
                .For(l => l.TextColor)
                .To(vm => vm.IsEmailInvalid)
                .WithConversion<EditTextValidationConverter>()
                .OneWay();

            set.Bind(_passwordTextField)
                .For(l => l.TextColor)
                .To(vm => vm.IsPasswordInvalid)
                .WithConversion<EditTextValidationConverter>()
                .OneWay();

            set.Apply();
        }

        private void OnLoginButtonTouched(object sender, EventArgs e)
        {
            View.EndEditing(true);
        }

        public override void ViewDidDisappear(bool animated)
        {
            _loginButton.TouchUpInside -= OnLoginButtonTouched;

            _emailTextField.ResetStyles();
            _passwordTextField.ResetStyles();

            _emailTextField.ShouldChangeCharacters -= OnShouldChangeCharacters;
            _passwordTextField.ShouldChangeCharacters -= OnShouldChangeCharacters;

            base.ViewDidDisappear(animated);
        }
    }
}

