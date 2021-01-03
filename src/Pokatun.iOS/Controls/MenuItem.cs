using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using CoreGraphics;
using Foundation;
using MvvmCross.UI;
using ObjCRuntime;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Controls
{
    [DesignTimeVisible(true)]
    public sealed partial class MenuItem : UIView
    {
        private const double ClickAnimationDuration = 0.062;

        private static readonly CGSize ContentSize = new CGSize(NoIntrinsicMetric, 60);

        private UIImage _image;
        private string _text;
        private string _additionalInfo;
        private MvxVisibility _additionalInfoVisibility;

        [DisplayName(nameof(Text)), Export("text"), Browsable(true)]
        public string Text
        {
            get { return _menuTextView == null ? _text : _menuTextView.Text; }
            set
            {
                _text = value;

                if (_menuTextView == null) return;

                _menuTextView.Text = value;
            }
        }

        [DisplayName(nameof(AdditionalInfo)), Export("additionalInfo"), Browsable(true)]
        public string AdditionalInfo
        {
            get { return _additionalInfoLabel == null ? _additionalInfo : _additionalInfoLabel.Text; }
            set
            {
                _additionalInfo = value;

                if (_additionalInfoLabel == null)
                    return;

                value = value ?? string.Empty;

                NSMutableAttributedString attributedString = new NSMutableAttributedString(value);

                foreach (Match m in Regex.Matches(value, Core.Constants.CirclePattern))
                {
                    attributedString.AddAttributes(
                        new UIStringAttributes { ForegroundColor = ColorPalette.FailValidationColor },
                        new NSRange(m.Index, m.Length)
                    );
                }

                _additionalInfoLabel.AttributedText = attributedString;
            }
        }

        [DisplayName(nameof(AdditionalInfoVisibility)), Export("additionalInfoVisibility"), Browsable(true)]
        public MvxVisibility AdditionalInfoVisibility
        {
            get { return _additionalInfoLabel == null ? _additionalInfoVisibility : _additionalInfoLabel.Hidden ? MvxVisibility.Hidden : MvxVisibility.Visible; }
            set
            {
                _additionalInfoVisibility = value;

                if (_additionalInfoLabel == null)
                    return;

                _additionalInfoLabel.Hidden = value == MvxVisibility.Visible ? false : true;
            }
        }

        [DisplayName(nameof(Image)), Export("image"), Browsable(true)]
        public UIImage Image
        {
            get { return _menuItemIcon?.Image; }
            set
            {
                _image = value;

                if (_menuItemIcon == null) return;

                _menuItemIcon.Image = value;
            }
        }

        public event EventHandler Clicked;

        public override CGSize IntrinsicContentSize => ContentSize;

        public MenuItem(IntPtr handle) : base(handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            NSArray nib = NSBundle.MainBundle.LoadNib(nameof(MenuItem), this, null);
            UIView rootView = Runtime.GetNSObject<UIView>(nib.ValueAt(0));

            AddSubview(rootView);

            Image = _image;
            Text = _text;
            AdditionalInfo = _additionalInfo;

            rootView.TranslatesAutoresizingMaskIntoConstraints = false;
            rootView.TopAnchor.ConstraintEqualTo(TopAnchor, 0).Active = true;
            rootView.LeftAnchor.ConstraintEqualTo(LeftAnchor, 0).Active = true;
            rootView.RightAnchor.ConstraintEqualTo(RightAnchor, 0).Active = true;
            rootView.BottomAnchor.ConstraintEqualTo(BottomAnchor, 0).Active = true;

            _menuTextView.ApplyLargeLabelStyle();
            _additionalInfoLabel.ApplyAdditionalInfoLabelStyle();

            AddGestureRecognizer(new UITapGestureRecognizer(OnItemTapped));
        }

        private async void OnItemTapped()
        {
            await AnimateAsync(ClickAnimationDuration, () => { Alpha = 0.5f; BackgroundColor = UIColor.LightGray; });
            await AnimateAsync(ClickAnimationDuration, () => { Alpha = 1f; BackgroundColor = UIColor.Clear; });
            Clicked?.Invoke(this, new EventArgs());
        }
    }
}

