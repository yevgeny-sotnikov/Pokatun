using System;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Controls
{
    [DesignTimeVisible(true)]
    [Register(nameof(BorderedButton))]
    public partial class BorderedButton : UIView
    {
        private const double ClickAnimationDuration = 0.125;
        private static readonly CGSize ContentSize = new CGSize(NoIntrinsicMetric, 40);

        private string _text;
        private UIColor _borderColor;
        private UIColor _textColor;
        private UIImage _leftImage;
        private bool _highlighted;
        private UIImage _rightImage;

        public string Text
        {
            get { return _label == null ? _text : _label.Text; }
            set
            {
                _text = value;

                if (_label == null)
                    return;

                _label.Text = value;
            }
        }

        [DisplayName(nameof(TextColor)), Export("textColor"), Browsable(true)]
        public UIColor TextColor
        {
            get { return _textColor; }
            set
            {
                _textColor = value;

                if (_label == null)
                    return;

                _label.TextColor = value;
            }
        }

        [DisplayName(nameof(HighlightedColor)), Export("highlightedColor"), Browsable(true)]
        public UIColor HighlightedColor { get; set; }

        [DisplayName(nameof(LeftImage)), Export("leftImage"), Browsable(true)]
        public UIImage LeftImage
        {
            get { return _leftImageView == null ? _leftImage : _leftImageView.Image; }
            set
            {
                _leftImage = value;

                if (_leftImageView == null)
                    return;

                _leftImageView.Image = value;
                _leftImageView.Hidden = value == null;
            }
        }

        [DisplayName(nameof(RightImage)), Export("rightImage"), Browsable(true)]
        public UIImage RightImage
        {
            get { return _rightImageView == null ? _rightImage : _rightImageView.Image; }
            set
            {
                _rightImage = value;

                if (_rightImageView == null)
                    return;

                _rightImageView.Image = value;
                _rightImageView.Hidden = value == null;
            }
        }

        public bool Highlighted
        {
            get { return _highlighted; }
            set
            {
                _highlighted = value;

                if (_label == null || _borderView == null)
                    return;

                _borderView.Highlighted = _highlighted;

                _label.TextColor = _highlighted ? HighlightedColor : TextColor;
            }
        }

        public event EventHandler Clicked;

        public override CGSize IntrinsicContentSize => ContentSize;

        public BorderedButton(IntPtr handle) : base(handle)
        {
            TextColor = UIColor.Black;
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            NSArray nib = NSBundle.MainBundle.LoadNib(nameof(BorderedButton), this, null);
            UIView rootView = Runtime.GetNSObject<UIView>(nib.ValueAt(0));

            AddSubview(rootView);

            this.Cornerize(_borderView.CornerRadius);

            LeftImage = _leftImage;
            RightImage = _rightImage;
            Text = _text;
            TextColor = _textColor;

            rootView.TranslatesAutoresizingMaskIntoConstraints = false;
            rootView.TopAnchor.ConstraintEqualTo(TopAnchor, 0).Active = true;
            rootView.LeftAnchor.ConstraintEqualTo(LeftAnchor, 0).Active = true;
            rootView.RightAnchor.ConstraintEqualTo(RightAnchor, 0).Active = true;
            rootView.BottomAnchor.ConstraintEqualTo(BottomAnchor, 0).Active = true;

            _borderView.ApplyBorderViewStyle();

            _label.TextAlignment = UITextAlignment.Center;
            _label.Font = Fonts.HelveticaNeueCyrLightExtraLarge;
            _label.TextColor = TextColor;
        }

        async partial void OnButtonTapped(NSObject sender)
        {
            await AnimateAsync(ClickAnimationDuration, () => { Alpha = 0f; BackgroundColor = ColorPalette.ButtonPressed; });
            await AnimateAsync(ClickAnimationDuration, () => { Alpha = 1f; BackgroundColor = UIColor.Clear; });
            Clicked?.Invoke(this, new EventArgs());
        }
    }
}
