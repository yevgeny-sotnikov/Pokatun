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
        private bool _selected;

        [DisplayName(nameof(Selected)), Export("selected"), Browsable(true)]
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
            }
        }

        [DisplayName(nameof(NibName)), Export("nibName"), Browsable(true)]
        public string NibName { get; set; }

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
        }
    }
}
