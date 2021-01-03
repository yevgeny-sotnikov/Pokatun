using System;
using Foundation;
using ObjCRuntime;
using Pokatun.iOS.Styles;
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

        public bool IsSubtitleHidden
        {
            get { return _subtitleLabel.Hidden; }
            set { _subtitleLabel.Hidden = value; }
        }

        public string Title
        {
            get { return _titleLabel.Text; }
            set { _titleLabel.Text = value; }
        }

        public string Subtitle
        {
            get { return _subtitleLabel.Text; }
            set { _subtitleLabel.Text = value; }
        }

        public bool SubtitleHightlighted
        {
            get { return _subtitleLabel.Highlighted; }
            set { _subtitleLabel.Highlighted = value; }
        }

        public TitleView(IntPtr handle) : base(handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            _titleLabel.ApplyTitleLabelStyle();

            _subtitleLabel.Font = Fonts.HelveticaNeueCyrLightMedium;
            _subtitleLabel.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;
            _subtitleLabel.TextColor = ColorPalette.SecondaryText;
            _subtitleLabel.HighlightedTextColor = ColorPalette.FailValidationColor;
        }
    }
}

