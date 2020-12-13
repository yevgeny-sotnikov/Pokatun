using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.ForgotPassword;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.ForgotPassword
{
    [MvxChildPresentation]
    public partial class NewPasswordViewController : BaseViewController<NewPasswordViewModel>
    {
        public NewPasswordViewController() : base(nameof(NewPasswordViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            _passwordEditText.ApplyBorderedEditTextStyle();
            _confirmPasswordEditText.ApplyBorderedEditTextStyle();

            _passwordEditText.KeyboardType = UIKeyboardType.Default;
            _passwordEditText.KeyboardType = UIKeyboardType.Default;

            _saveButton.ApplyBigButtonStyle();

            _saveButton.TouchUpInside += OnButtonTouched;

            _passwordEditText.Placeholder = Strings.Password;
            _confirmPasswordEditText.Placeholder = Strings.ConfirmPassword;
            _saveButton.SetTitle(Strings.SavePassword, UIControlState.Normal);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(_passwordEditText).For(v => v.Text).To(vm => vm.Password).OneWayToSource();
            set.Bind(_confirmPasswordEditText).For(v => v.Text).To(vm => vm.PasswordConfirm).OneWayToSource();
            set.Bind(_saveButton).To(vm => vm.SavePasswordCommand);

            set.Bind(_passwordEditText).For(v => v.Highlighted).To(vm => vm.IsPasswordInvalid).OneWay();
            set.Bind(_confirmPasswordEditText).For(v => v.Highlighted).To(vm => vm.IsPasswordInvalid).OneWay();

            set.Apply();
        }

        public override void ViewDidDisappear(bool animated)
        {
            _saveButton.TouchUpInside -= OnButtonTouched;

            base.ViewDidDisappear(animated);
        }

        private void OnButtonTouched(object sender, EventArgs e)
        {
            View.EndEditing(true);
        }
    }
}

