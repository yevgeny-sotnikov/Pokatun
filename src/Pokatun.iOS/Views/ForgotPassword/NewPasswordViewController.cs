using System;
using System.Collections.Generic;
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

            _passwordEditText.ShouldChangeCharacters += OnShouldChangeCharacters;
            _confirmPasswordEditText.ShouldChangeCharacters += OnShouldChangeCharacters;

            _passwordEditText.Placeholder = Strings.Password;
            _confirmPasswordEditText.Placeholder = Strings.ConfirmPassword;
            _saveButton.SetTitle(Strings.SavePassword, UIControlState.Normal);
        }

        public override void ViewDidDisappear(bool animated)
        {
            _passwordEditText.ResetStyles();
            _confirmPasswordEditText.ResetStyles();

            _passwordEditText.ShouldChangeCharacters -= OnShouldChangeCharacters;
            _confirmPasswordEditText.ShouldChangeCharacters -= OnShouldChangeCharacters;

            base.ViewDidDisappear(animated);
        }
    }
}

