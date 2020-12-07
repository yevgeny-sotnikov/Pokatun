using System;
using System.ComponentModel;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Pokatun.iOS.Controls.Tabs
{
    [DesignTimeVisible(true)]
    [Register(nameof(HotelProfileTab))]
    public partial class HotelProfileTab : UIView
    {
        [DisplayName(nameof(Selected)), Export("selected"), Browsable(true)]
        public bool Selected { get; set; }

        [DisplayName(nameof(NibName)), Export("nibName"), Browsable(true)]
        public string NibName { get; set; }

        [DisplayName(nameof(TabContentViewTag)), Export("tabContentViewTag"), Browsable(true)]
        public nint TabContentViewTag { get; set; }

        public event EventHandler Tapped;

        public HotelProfileTab(IntPtr handle) : base(handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            NSArray nib = NSBundle.MainBundle.LoadNib(NibName, this, null);
            UIView rootView = Runtime.GetNSObject<UIView>(nib.ValueAt(0));

            AddSubview(rootView);

            rootView.TranslatesAutoresizingMaskIntoConstraints = false;
            rootView.TopAnchor.ConstraintEqualTo(TopAnchor, 0).Active = true;
            rootView.LeftAnchor.ConstraintEqualTo(LeftAnchor, 0).Active = true;
            rootView.RightAnchor.ConstraintEqualTo(RightAnchor, 0).Active = true;
            rootView.BottomAnchor.ConstraintEqualTo(BottomAnchor, 0).Active = true;

            rootView.AddGestureRecognizer(new UITapGestureRecognizer(OnTapped));
        }

        private void OnTapped()
        {
            Tapped?.Invoke(this, new EventArgs());
        }
    }
}
