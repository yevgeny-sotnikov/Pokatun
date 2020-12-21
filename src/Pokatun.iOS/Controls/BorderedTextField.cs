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
    [Register(nameof(BorderedTextField))]
    public sealed partial class BorderedTextField : UIView
    {
        private static readonly CGSize ContentSize = new CGSize(NoIntrinsicMetric, 40);

        private UIImage _leftImage;
        private string _placeholderStr;
        private string _text;
        private bool _highlighted;
        private bool _inEditMode;
        private UIKeyboardType _keyboardType;
        private UIColor _textColor;
        private UIColor _borderColor;
        private bool _secureTextEntry;
        private bool _rightButtonHidden = true;

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

        [DisplayName(nameof(TextColor)), Export("textColor"), Browsable(true)]
        public UIColor TextColor
        {
            get { return _textColor; }
            set
            {
                _textColor = value;

                if (_textField == null) return;

                _textField.TextColor = value;
            }
        }

        [DisplayName(nameof(SelectionColor)), Export("selectionColor"), Browsable(true)]
        public UIColor SelectionColor { get; set; }

        [DisplayName(nameof(HighlightedColor)), Export("validationFailedColor"), Browsable(true)]
        public UIColor HighlightedColor { get; set; }

        [DisplayName(nameof(MaxLenght)), Export("maxLenght"), Browsable(true)]
        public nint MaxLenght { get; set; }

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

        [DisplayName(nameof(RightButtonHidden)), Export("rightButtonHidden"), Browsable(true)]
        public bool RightButtonHidden
        {
            get { return _rightButton == null ? _rightButtonHidden : _rightButton.Hidden; }
            set
            {
                _rightButtonHidden = value;

                if (_rightButton == null)
                    return;

                _rightButton.Hidden = value;
            }
        }

        public event EventHandler RightButtonClicked;

        [DisplayName(nameof(KeyboardType)), Export("keyboardType"), Browsable(true)]
        public UIKeyboardType KeyboardType
        {
            get { return _textField == null ? _keyboardType : _textField.KeyboardType; }
            set
            {
                _keyboardType = value;

                if (_textField == null) return;

                _textField.KeyboardType = _keyboardType;
            }
        }

        [DisplayName(nameof(SecureTextEntry)), Export("secureTextEntry"), Browsable(true)]
        public bool SecureTextEntry
        {
            get { return _textField == null ? _secureTextEntry : _textField.SecureTextEntry; }
            set
            {
                _secureTextEntry = value;

                if (_textField == null)
                    return;

                _textField.SecureTextEntry = value;
            }
        }

        public string Text
        {
            get { return _textField == null ? _text : _textField.Text; }
            set
            {
                _text = value;

                if (_textField == null) return;

                _textField.Text = value;
            }
        }

        public event EventHandler TextChanged;

        public string Placeholder
        {
            get { return _textField == null ? _placeholderStr : _textField.Placeholder; }
            set
            {
                _placeholderStr = value;

                if (_textField == null)
                    return;

                _textField.Placeholder = value;
            }
        }

        public bool Highlighted
        {
            get { return _highlighted; }
            set
            {
                _highlighted = value;

                if (_textField == null)
                    return;

                UIColor color = _highlighted ? HighlightedColor : BorderColor;
                
                Layer.BorderColor = _inEditMode ? SelectionColor.CGColor : color.CGColor;
                _textField.TextColor = color;
            }
        }

        public override CGSize IntrinsicContentSize => ContentSize;

        public BorderedTextField(IntPtr handle) : base(handle)
        {
            BorderColor = UIColor.Black;
            TextColor = UIColor.Black;
            SelectionColor = UIColor.Blue;
            HighlightedColor = UIColor.Red;
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            NSArray nib = NSBundle.MainBundle.LoadNib(nameof(BorderedTextField), this, null);
            UIView rootView = Runtime.GetNSObject<UIView>(nib.ValueAt(0));

            AddSubview(rootView);

            LeftImage = _leftImage;
            RightButtonHidden = _rightButtonHidden;
            Text = _text;
            Placeholder = _placeholderStr;
            Highlighted = _highlighted;
            KeyboardType = _keyboardType;
            TextColor = _textColor;
            SecureTextEntry = _secureTextEntry;

            rootView.TranslatesAutoresizingMaskIntoConstraints = false;
            rootView.TopAnchor.ConstraintEqualTo(TopAnchor, 0).Active = true;
            rootView.LeftAnchor.ConstraintEqualTo(LeftAnchor, 0).Active = true;
            rootView.RightAnchor.ConstraintEqualTo(RightAnchor, 0).Active = true;
            rootView.BottomAnchor.ConstraintEqualTo(BottomAnchor, 0).Active = true;

            this.Cornerize(19);
            Layer.BorderWidth = 1;
            Layer.BorderColor = BorderColor.CGColor;
            _textField.TextAlignment = UITextAlignment.Center;
            _textField.Font = Fonts.HelveticaNeueCyrLightExtraLarge;
            _textField.TextColor = TextColor;
            _textField.SetTextContentType(UITextContentType.NewPassword);

            _textField.EditingChanged += OnEditingChanged;
            _textField.EditingDidBegin += OnEditingDidBegin;
            _textField.EditingDidEnd += OnEditingDidEnd;
            _textField.ShouldChangeCharacters += OnShouldChangeCharacters;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _textField.EditingChanged -= OnEditingChanged;
                _textField.EditingDidBegin -= OnEditingDidBegin;
                _textField.EditingDidEnd -= OnEditingDidEnd;
                _textField.ShouldChangeCharacters -= OnShouldChangeCharacters;
            }

            base.Dispose(disposing);
        }

        private void OnEditingChanged(object sender, EventArgs e)
        {
            TextChanged?.Invoke(this, e);
        }

        private void OnEditingDidBegin(object sender, EventArgs e)
        {
            _inEditMode = true;
            _textField.TextColor = TextColor;

            Layer.BorderWidth = 2;
            Layer.BorderColor = SelectionColor.CGColor;
        }

        private void OnEditingDidEnd(object sender, EventArgs e)
        {
            _inEditMode = false;

            Layer.BorderWidth = 1;
            Layer.BorderColor = BorderColor.CGColor;
        }

        private bool OnShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
        {
            nint newLength = textField.Text.Length + replacementString.Length - range.Length;

            return newLength <= MaxLenght;
        }

        partial void OnRightButtonTouchUpInside(Foundation.NSObject sender)
        {
            RightButtonClicked?.Invoke(this, new EventArgs());
        }
    }
}
