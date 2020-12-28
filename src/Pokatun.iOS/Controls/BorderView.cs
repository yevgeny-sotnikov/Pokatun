using System;
using System.ComponentModel;
using Foundation;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS
{
    [DesignTimeVisible(true)]
    [Register(nameof(BorderView))]

    public class BorderView : UIView
    {
        private UIColor _borderColor;
        private bool _highlighted;
        private bool _selected;
        private bool _inEditMode;
        private nfloat _cornerRadius = 19;

        [DisplayName(nameof(CornerRadius)), Export("cornerRadius"), Browsable(true)]
        public nfloat CornerRadius
        {
            get { return _cornerRadius; }
            set
            {
                _cornerRadius = value;
                this.Cornerize(_cornerRadius);
            }
        }

        [DisplayName(nameof(BorderColor)), Export("borderColor"), Browsable(true)]
        public UIColor BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                Layer.BorderColor = value.CGColor;
            }
        }

        [DisplayName(nameof(SelectionColor)), Export("selectionColor"), Browsable(true)]
        public UIColor SelectionColor { get; set; }

        [DisplayName(nameof(HighlightedColor)), Export("highlightedColor"), Browsable(true)]
        public UIColor HighlightedColor { get; set; }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;

                if (_selected)
                {
                    Layer.BorderWidth = 2;
                    Layer.BorderColor = SelectionColor.CGColor;
                }
                else
                {
                    Layer.BorderWidth = 1;
                    Layer.BorderColor = BorderColor.CGColor;
                }
            }
        }

        public bool Highlighted
        {
            get { return _highlighted; }
            set
            {
                _highlighted = value;

                UIColor color = _highlighted ? HighlightedColor : BorderColor;

                Layer.BorderColor = InEditMode ? SelectionColor.CGColor : color.CGColor;
            }
        }

        public bool InEditMode
        {
            get { return _inEditMode; }
            set
            {
                _inEditMode = value;

                if (_inEditMode)
                {
                    Layer.BorderWidth = 2;
                    Layer.BorderColor = SelectionColor.CGColor;
                }
                else
                {
                    Layer.BorderWidth = 1;
                    Layer.BorderColor = BorderColor.CGColor;
                }
            }
        }

        public BorderView(IntPtr handle) : base(handle)
        {
            BorderColor = UIColor.Black;
            SelectionColor = UIColor.Blue;
            HighlightedColor = UIColor.Red;
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            Layer.BorderWidth = 1;
            this.Cornerize(_cornerRadius);
        }
    }
}
