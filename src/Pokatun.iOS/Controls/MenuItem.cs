using System;
using System.ComponentModel;
using Foundation;
using UIKit;

namespace Pokatun.iOS.Controls
{
    [DesignTimeVisible(true)]
    public sealed partial class MenuItem : UIView
    {
        public MenuItem(IntPtr handle) : base(handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            NSBundle.MainBundle.LoadNib(nameof(MenuItem), this, null);
            AddSubview(_rootView);

            _rootView.TranslatesAutoresizingMaskIntoConstraints = false;
            _rootView.TopAnchor.ConstraintEqualTo(TopAnchor, 0).Active = true;
            _rootView.LeftAnchor.ConstraintEqualTo(LeftAnchor, 0).Active = true;
            _rootView.RightAnchor.ConstraintEqualTo(RightAnchor, 0).Active = true;
            _rootView.BottomAnchor.ConstraintEqualTo(BottomAnchor, 0).Active = true;

            //Initialise();
        }
    }
}

