using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.PreEntrance;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.PreEntrance
{
    [MvxChildPresentation]
    public sealed partial class PreEntranceViewController : BaseViewController<PreEntranceViewModel>
    {
        public PreEntranceViewController() : base(nameof(PreEntranceViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            _helloLabel.ApplySubGiganticLabelStyle();
            _preEntranceDescriptionLabel.ApplyLargeLabelStyle();
            _iStillDoesntHaveAccLabel.ApplyLargeLabelStyle();
            _iHaveAccLabel.ApplyLargeLabelStyle();

            _registrationButton.ApplyBigButtonStyle();
            _loginButton.ApplyBigButtonStyle();

            _helloLabel.Text = Strings.HelloCaps;
            _registrationButton.SetTitle(Strings.Registration, UIControlState.Normal);
            _iStillDoesntHaveAccLabel.Text = Strings.IStillDoesntHaveAccText;
            _loginButton.SetTitle(Strings.Entrance, UIControlState.Normal);
            _iHaveAccLabel.Text = Strings.IHaveAccText;

            MvxFluentBindingDescriptionSet<IMvxIosView<PreEntranceViewModel>, PreEntranceViewModel> set = CreateBindingSet();

            set.Bind(_preEntranceDescriptionLabel).To(vm => vm.PreEntranceDescriptionText);
            set.Bind(_registrationButton).To(vm => vm.RegistrationCommand);
            set.Bind(_loginButton).To(vm => vm.LoginCommand);

            set.Apply();
        }
    }
}

