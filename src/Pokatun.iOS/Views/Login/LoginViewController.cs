using System;
using System.Collections.Generic;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Login;
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
        }

        public override void ViewDidDisappear(bool animated)
        {
            _emailTextField.ResetStyles();
            _passwordTextField.ResetStyles();

            _emailTextField.ShouldChangeCharacters -= OnShouldChangeCharacters;
            _passwordTextField.ShouldChangeCharacters -= OnShouldChangeCharacters;

            base.ViewDidDisappear(animated);
        }
    }
}

