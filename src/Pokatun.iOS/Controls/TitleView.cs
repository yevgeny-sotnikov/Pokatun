using System;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Pokatun.iOS.Controls
{
    public partial class TitleView : UIView
    {
        public static TitleView Create()
        {
            NSArray nib = NSBundle.MainBundle.LoadNib(nameof(TitleView), null, null);

            TitleView view = Runtime.GetNSObject<TitleView>(nib.ValueAt(0));

            return view;
        }

        public bool IsLogoHidden
        {
            get { return _logo.Hidden; }
            set { _logo.Hidden = value; }
        }

        public bool IsTitleHidden
        {
            get { return _titleLabel.Hidden; }
            set { _titleLabel.Hidden = value; }
        }

        public string Title
        {
            get { return _titleLabel.Text; }
            set { _titleLabel.Text = value; }
        }

        public TitleView(IntPtr handle) : base(handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            _titleLabel.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;
            _subtitleLabel.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;
        }
    }
}

