using System;
using Foundation;
using Pokatun.Core.ViewModels;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.ForgotPassword
{
    public partial class VerificationCodeViewController : UIViewController
    {
        public UIButton Button => _button;

        public UILabel DescriptionLabel => _descriptionLabel;

        public UITextField TextField => _textField;

        public int MaxTextFieldLenght { get; set; }

        public VerificationCodeViewController() : base(nameof(VerificationCodeViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            TextField.ApplyBorderedEditTextStyle();
            DescriptionLabel.ApplyLargeLabelStyle();
            Button.ApplyBigButtonStyle();

            Button.TouchUpInside += OnTouchButtonInside;
            TextField.ShouldChangeCharacters += OnShouldChangeCharacters;
        }

        public override void ViewDidDisappear(bool animated)
        {
            TextField.ResetStyles();
            Button.TouchUpInside -= OnTouchButtonInside;
            TextField.ShouldChangeCharacters -= OnShouldChangeCharacters;

            base.ViewDidDisappear(animated);
        }

        private void OnTouchButtonInside(object sender, EventArgs e)
        {
            View.EndEditing(true);
        }

        private bool OnShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
        {
            nint newLength = textField.Text.Length + replacementString.Length - range.Length;

            return newLength <= MaxTextFieldLenght;
        }
    }
}

