using System;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Controls
{
    [DesignTimeVisible(true)]
    public sealed partial class MenuItem : UIView
    {
        private static readonly CGSize ContentSize = new CGSize(NoIntrinsicMetric, 85);

        public string Text
        {
            get { return _menuTextView.Text; }
            set { _menuTextView.Text = value; }
        }

        public UIImage Image
        {
            get { return _menuItemIcon.Image; }
            set { _menuItemIcon.Image = value; }
        }

        public override CGSize IntrinsicContentSize => ContentSize;

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

            _menuTextView.ApplyLargeLabelStyle();
        }
    }
}

