using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
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

            _helloLabel.Font = Fonts.HelveticaNeueCyrLightSubGigantic;
            _registrationButton.Font = Fonts.HelveticaNeueCyrLightGigantic;
            _loginButton.Font = Fonts.HelveticaNeueCyrLightGigantic;


            _helloLabel.Text = Strings.HelloCaps;
            _preEntranceDescriptionLabel.Text = Strings.PreEntranceDescriptionText;
            _registrationButton.SetTitle(Strings.Registration, UIControlState.Normal);
            _iStillDoesntHaveAccLabel.Text = Strings.IStillDoesntHaveAccText;
            _loginButton.SetTitle(Strings.Entrance, UIControlState.Normal);
            _iHaveAccLabel.Text = Strings.IHaveAccText;
        }
    }
}

