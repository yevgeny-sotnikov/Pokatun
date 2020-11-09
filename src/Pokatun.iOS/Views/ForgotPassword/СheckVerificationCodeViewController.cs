using System;
using Pokatun.Core.ViewModels.ForgotPassword;

namespace Pokatun.iOS.Views.ForgotPassword
{
    public sealed class СheckVerificationCodeViewController : BaseViewController<СheckVerificationCodeViewModel>
    {
        private VerificationCodeViewController _controller;

        public СheckVerificationCodeViewController() : base(null, null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            _controller = new VerificationCodeViewController();

            AddChildViewController(_controller);
            View.AddSubview(_controller.View);
            _controller.View.Frame = View.Frame;
            _controller.DidMoveToParentViewController(this);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            //set.Bind(_controller.Button).To(vm => vm.RequestCodeCommand);
        }

    }
}
