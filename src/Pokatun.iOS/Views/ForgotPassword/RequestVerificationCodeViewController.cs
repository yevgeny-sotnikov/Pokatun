using System;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.ForgotPassword;
using UIKit;

namespace Pokatun.iOS.Views.ForgotPassword
{
    public sealed class RequestVerificationCodeViewController : BaseViewController<RequestVerificationCodeViewModel>
    {
        private const int MaxEmailLenght = 64;

        private VerificationCodeViewController _controller;

        public RequestVerificationCodeViewController() : base(null, null)
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

            _controller.TextField.Placeholder = Strings.Email;
            _controller.TextField.KeyboardType = UIKeyboardType.EmailAddress;
            _controller.MaxTextFieldLenght = MaxEmailLenght;
            _controller.DescriptionLabel.Text = Strings.WriteRegistrationEmailMessage;
            _controller.Button.SetTitle(Strings.GetCode, UIControlState.Normal);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(_controller.Button).To(vm => vm.RequestCodeCommand);
            set.Bind(_controller.Button).For(b => b.Enabled).To(vm => vm.IsRequestCodeButtonEnabled);
            set.Bind(_controller.TextField).To(vm => vm.Email).OneWayToSource();

            set.Apply();
        }
    }
}

