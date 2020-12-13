using System;
using Pokatun.iOS.Controls;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.ForgotPassword
{
    public partial class VerificationCodeViewController : UIViewController
    {
        public UIButton Button => _button;

        public UILabel DescriptionLabel => _descriptionLabel;

        public BorderedTextField TextField => _textField;

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
        }

        public override void ViewDidDisappear(bool animated)
        {
            Button.TouchUpInside -= OnTouchButtonInside;

            base.ViewDidDisappear(animated);
        }

        private void OnTouchButtonInside(object sender, EventArgs e)
        {
            View.EndEditing(true);
        }
    }
}

