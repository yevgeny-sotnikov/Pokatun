using System;
using Pokatun.Core.ViewModels;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.ForgotPassword
{
    public partial class VerificationCodeViewController : UIViewController
    {
        public UIButton Button => _button;

        public VerificationCodeViewController() : base(nameof(VerificationCodeViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            Button.ApplyBigButtonStyle();
        }
    }
}

