using System;
using System.ComponentModel;
using Foundation;
using ObjCRuntime;
using Pokatun.Core.Resources;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Controls
{
    [DesignTimeVisible(true)]
    public partial class MultilineCountableEditor : UIView
    {
        private int _maxLenght;
        private UIColor _textColor;
        private bool _highlighted;

        [DisplayName(nameof(TextColor)), Export("textColor"), Browsable(true)]
        public UIColor TextColor
        {
            get { return _textColor; }
            set
            {
                _textColor = value;

                if (_dataTextField == null)
                    return;

                _dataTextField.TextColor = value;
            }
        }

        [DisplayName(nameof(HighlightedColor)), Export("highlightedColor"), Browsable(true)]
        public UIColor HighlightedColor { get; set; }

        public string Title
        {
            get { return _titleLabel.Text; }
            set { _titleLabel.Text = value; }
        }

        public string Data
        {
            get { return _dataTextField.Text; }
            set
            {
                _dataTextField.Text = value;

                _hintLabel.Hidden = !string.IsNullOrEmpty(value);
                SetupCounterText(MaxLenght - value.Length);
            }
        }

        public string Hint
        {
            get { return _hintLabel.Text; }
            set
            {
                _hintLabel.Text = value;
            }
        }


        public int MaxLenght
        {
            get { return _maxLenght; }
            set
            {
                _maxLenght = value;
                SetupCounterText(value - Data.Length);
            }
        }

        public bool Highlighted
        {
            get { return _highlighted; }
            set
            {
                _highlighted = value;

                if (_dataTextField == null || _borderView == null)
                    return;

                _borderView.Highlighted = _highlighted;

                _dataTextField.TextColor = _highlighted ? HighlightedColor : TextColor;
                _titleLabel.TextColor = _highlighted ? HighlightedColor : ColorPalette.PrimaryText;
                _hintLabel.TextColor = _highlighted ? HighlightedColor : ColorPalette.BorderColor;
                _counterLabel.TextColor = _highlighted ? HighlightedColor : ColorPalette.PrimaryText;
            }
        }


        public event EventHandler DataChanged;

        public MultilineCountableEditor(IntPtr handle) : base(handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            NSArray nib = NSBundle.MainBundle.LoadNib(nameof(MultilineCountableEditor), this, null);
            UIView rootView = Runtime.GetNSObject<UIView>(nib.ValueAt(0));

            AddSubview(rootView);

            rootView.TranslatesAutoresizingMaskIntoConstraints = false;
            rootView.TopAnchor.ConstraintEqualTo(TopAnchor, 0).Active = true;
            rootView.LeftAnchor.ConstraintEqualTo(LeftAnchor, 0).Active = true;
            rootView.RightAnchor.ConstraintEqualTo(RightAnchor, 0).Active = true;
            rootView.BottomAnchor.ConstraintEqualTo(BottomAnchor, 0).Active = true;

            _borderView.ApplyBorderViewStyle();

            _titleLabel.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;
            _titleLabel.Font = Fonts.HelveticaNeueCyrLightExtraLarge;
            _titleLabel.TextColor = ColorPalette.PrimaryText;

            _dataTextField.Font = Fonts.HelveticaNeueCyrLightMedium;
            _dataTextField.TextColor = ColorPalette.PrimaryText;

            _hintLabel.Font = Fonts.HelveticaNeueCyrLightMedium;
            _hintLabel.TextColor = ColorPalette.BorderColor;

            _counterLabel.Font = Fonts.HelveticaNeueCyrLightMedium;
            _hintLabel.TextColor = ColorPalette.PrimaryText;

            TextColor = _textColor;
            HighlightedColor = ColorPalette.FailValidationColor;

            _dataTextField.Started += OnEditingStarted;
            _dataTextField.Ended += OnEditingEnded;
            _dataTextField.Changed += OnTextChanged;
            _dataTextField.ShouldChangeText += OnShouldChangeText;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataTextField.Started -= OnEditingStarted;
                _dataTextField.Ended -= OnEditingEnded;
                _dataTextField.Changed -= OnTextChanged;
                _dataTextField.ShouldChangeText -= OnShouldChangeText;
            }

            base.Dispose(disposing);
        }

        partial void OnWrapperAreaTapped(NSObject sender)
        {
            _dataTextField.BecomeFirstResponder();
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            DataChanged?.Invoke(this, e);

            int dataLenght = Data.Length;

            SetupCounterText(MaxLenght - dataLenght);

            _hintLabel.Hidden = dataLenght > 0;
        }

        private void OnEditingStarted(object sender, EventArgs e)
        {
            _borderView.InEditMode = true;

            _dataTextField.TextColor = TextColor;
            _titleLabel.TextColor = ColorPalette.PrimaryText;
            _hintLabel.TextColor = ColorPalette.BorderColor;
            _counterLabel.TextColor = ColorPalette.PrimaryText;
        }

        private void OnEditingEnded(object sender, EventArgs e)
        {
            _borderView.InEditMode = false;
        }

        private bool OnShouldChangeText(UITextView textView, NSRange range, string text)
        {
            nint newLength = textView.Text.Length + text.Length - range.Length;

            return newLength <= MaxLenght;
        }

        private void SetupCounterText(int counterVal)
        {
            _counterLabel.Text = string.Format(Strings.SymbolsLeftFormat, counterVal);
        }
    }
}

