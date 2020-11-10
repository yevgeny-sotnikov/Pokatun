using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.ViewModels.ForgotPassword;
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
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

