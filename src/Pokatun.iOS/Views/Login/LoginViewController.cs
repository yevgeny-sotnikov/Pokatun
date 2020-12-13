using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Login;
using Pokatun.iOS.Controls;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.Login
{
    [MvxChildPresentation]
    public sealed partial class LoginViewController : BaseViewController<LoginViewModel>
    {
        public LoginViewController() : base(nameof(LoginViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            _emailTextField.ApplyBorderedEditTextStyle();
            _passwordTextField.ApplyBorderedEditTextStyle();

            _emailTextField.KeyboardType = UIKeyboardType.EmailAddress;
            _passwordTextField.KeyboardType = UIKeyboardType.Default;

            _forgotPasswordButton.Font = Fonts.HelveticaNeueCyrBoldLarge;
            _forgotPasswordButton.TintColor = ColorPalette.PrimaryLight;

            _loginButton.ApplyBigButtonStyle();

            _emailTextField.Placeholder = Strings.Email;
            _passwordTextField.Placeholder = Strings.Password;
            _forgotPasswordButton.SetTitle(Strings.ForgotPasswordQuestion, UIControlState.Normal);
            _loginButton.SetTitle(Strings.ToComeIn, UIControlState.Normal);

            _loginButton.TouchUpInside += OnLoginButtonTouched;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(_emailTextField).For(v => v.Text).To(vm => vm.Email).OneWayToSource();
            set.Bind(_passwordTextField).For(v => v.Text).To(vm => vm.Password).OneWayToSource();
            set.Bind(_loginButton).To(vm => vm.LoginCommand);
            set.Bind(_forgotPasswordButton).To(vm => vm.ForgotPasswordCommand);

            set.Bind(_emailTextField).For(v => v.Highlighted).To(vm => vm.IsEmailInvalid).OneWay();
            set.Bind(_passwordTextField).For(v => v.Highlighted).To(vm => vm.IsPasswordInvalid).OneWay();

            set.Apply();
        }

        private void OnLoginButtonTouched(object sender, EventArgs e)
        {
            View.EndEditing(true); 
        }

        public override void ViewDidDisappear(bool animated)
        {
            _loginButton.TouchUpInside -= OnLoginButtonTouched;

            base.ViewDidDisappear(animated);
        }
    }
}

