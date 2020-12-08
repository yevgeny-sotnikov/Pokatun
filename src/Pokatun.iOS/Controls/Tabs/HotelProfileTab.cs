using System;
using System.ComponentModel;
using Foundation;
using ObjCRuntime;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Controls.Tabs
{
    [DesignTimeVisible(true)]
    [Register(nameof(HotelProfileTab))]
    public partial class HotelProfileTab : UIView
    {
        private const double TapAnimationDuration = 0.2;
        private static readonly nfloat SelectedAlpha = 1;
        private static readonly nfloat UnselectedAlpha = 0.25f;
        
        private bool _selected = false;

        [DisplayName(nameof(Selected)), Export("selected"), Browsable(true)]
        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (_selected == value) return;

                _selected = value;

                if (_imageView == null || _label == null) return;

                if (value)
                {
                     Animate(TapAnimationDuration, () => Alpha = SelectedAlpha);
                }
                else
                {
                    Animate(TapAnimationDuration, () => Alpha = UnselectedAlpha);
                }
            }
        }

        [DisplayName(nameof(NibName)), Export("nibName"), Browsable(true)]
        public string NibName { get; set; }

        [DisplayName(nameof(TabContentViewTag)), Export("tabContentViewTag"), Browsable(true)]
        public nint TabContentViewTag { get; set; }

        public string Text
        {
            get { return _label.Text; }
            set { _label.Text = value; }
        }

        public event EventHandler Tapped;

        public HotelProfileTab(IntPtr handle) : base(handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            NSArray nib = NSBundle.MainBundle.LoadNib(NibName, this, null);
            UIView rootView = Runtime.GetNSObject<UIView>(nib.ValueAt(0));

            _label.ApplyInfoTabLabelStyle();

            AddSubview(rootView);

            Alpha = Selected ? SelectedAlpha : UnselectedAlpha;

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
