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
        private UIKeyboardType _keyboardType;
        private UIColor _textColor;
        private bool _secureTextEntry;
        private bool _rightButtonHidden = true;

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

        [DisplayName(nameof(HighlightedColor)), Export("highlightedColor"), Browsable(true)]
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

                if (_textField == null || _borderView == null)
                    return;

                _borderView.Highlighted = _highlighted;

                _textField.TextColor = _highlighted ? HighlightedColor : TextColor;
            }
        }

        public override CGSize IntrinsicContentSize => ContentSize;

        public BorderedTextField(IntPtr handle) : base(handle)
        {
            TextColor = UIColor.Black;
            HighlightedColor = UIColor.Red;
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            NSArray nib = NSBundle.MainBundle.LoadNib(nameof(BorderedTextField), this, null);
            UIView rootView = Runtime.GetNSObject<UIView>(nib.ValueAt(0));

            AddSubview(rootView);

            rootView.TranslatesAutoresizingMaskIntoConstraints = false;
            rootView.TopAnchor.ConstraintEqualTo(TopAnchor, 0).Active = true;
            rootView.LeftAnchor.ConstraintEqualTo(LeftAnchor, 0).Active = true;
            rootView.RightAnchor.ConstraintEqualTo(RightAnchor, 0).Active = true;
            rootView.BottomAnchor.ConstraintEqualTo(BottomAnchor, 0).Active = true;

            _borderView.ApplyBorderViewStyle();
            _textField.VerticalAlignment = UIControlContentVerticalAlignment.Center;

            _textField.TextAlignment = UITextAlignment.Center;
            _textField.Font = Fonts.HelveticaNeueCyrLightExtraLarge;
            _textField.TextColor = TextColor;
            _textField.SetTextContentType(UITextContentType.Nickname);

            LeftImage = _leftImage;
            RightButtonHidden = _rightButtonHidden;
            Text = _text;
            Placeholder = _placeholderStr;
            Highlighted = _highlighted;
            KeyboardType = _keyboardType;
            TextColor = _textColor;
            SecureTextEntry = _secureTextEntry;

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
            _borderView.InEditMode = true;
            _textField.TextColor = TextColor;
        }

        private void OnEditingDidEnd(object sender, EventArgs e)
        {
            _borderView.InEditMode = false;
        }

        private bool OnShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
        {
            if (MaxLenght == 0)
            {
                return true;
            }

            nint newLength = textField.Text.Length + replacementString.Length - range.Length;

            return newLength <= MaxLenght;
        }

        partial void OnRightButtonTouchUpInside(NSObject sender)
        {
            RightButtonClicked?.Invoke(this, new EventArgs());
        }
    }
}
