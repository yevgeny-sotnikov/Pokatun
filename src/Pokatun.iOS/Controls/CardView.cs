using System;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Pokatun.iOS.Controls
{
    /// <summary>
    /// A simple view with child views to have a set shadow in design time.
    /// This can be imported into a storyboard or xib with these design time variables
    /// </summary>
    [Register(nameof(CardView)), DesignTimeVisible(true)]
    public class CardView : UIView
    {
        private float _cornerRadius;
        private UIColor _shadowColor;
        private int _shadowOffsetHeight;
        private int _shadowOffsetWidth;
        private float _shadowOpacity;

        [DisplayName("Shadow Opacity"), Export("shadowOpacity"), Browsable(true)]
        public float ShadowOpacity
        {
            get { return _shadowOpacity; }
            set
            {
                _shadowOpacity = value;
                SetNeedsDisplay();
            }
        }

        [DisplayName("Shadow Offset Width"), Export("shadowOffsetWidth"), Browsable(true)]
        public int ShadowOffsetWidth
        {
            get { return _shadowOffsetWidth; }
            set
            {
                _shadowOffsetWidth = value;
                SetNeedsDisplay();
            }
        }

        [DisplayName("Shadow Offset Height"), Export("shadowOffsetHeight"), Browsable(true)]
        public int ShadowOffsetHeight
        {
            get { return _shadowOffsetHeight; }
            set
            {
                _shadowOffsetHeight = value;
                SetNeedsDisplay();
            }
        }

        [DisplayName("Corner Radius"), Export("cornerRadius"), Browsable(true)]
        public float CornerRadius
        {
            get { return _cornerRadius; }
            set
            {
                _cornerRadius = value;
                SetNeedsDisplay();
            }
        }

        [DisplayName("Shadow Color"), Export("shadowColor"), Browsable(true)]
        public UIColor ShadowColor
        {
            get { return _shadowColor; }
            set
            {
                _shadowColor = value;
                SetNeedsDisplay();
            }
        }

        public CardView()
        {
            Initialize();
        }

        public CardView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        protected CardView(NSObjectFlag t) : base(t)
        {
            Initialize();
        }

        protected internal CardView(IntPtr handle) : base(handle)
        {
        }

        public CardView(CGRect frame) : base(frame)
        {
            Initialize();
        }


        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            Initialize();
        }

        private void Initialize()
        {
            ShadowColor = UIColor.Gray;
            CornerRadius = 0;
            ShadowOffsetHeight = 1;
            ShadowOffsetWidth = 0;
            ShadowOpacity = 0.2f;
            SetupCard();
        }

        private void SetupCard()
        {
            Layer.CornerRadius = CornerRadius;
            UIBezierPath bezierPath = UIBezierPath.FromRoundedRect(Bounds, CornerRadius);
            Layer.MasksToBounds = false;
            Layer.ShadowColor = ShadowColor.CGColor;
            Layer.ShadowOffset = new CGSize(_shadowOffsetWidth, _shadowOffsetHeight);
            Layer.ShadowOpacity = _shadowOpacity;
            Layer.ShadowPath = bezierPath.CGPath;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            SetupCard();
        }
    }
}
