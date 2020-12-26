using System;
using System.Linq;
using Android.Content;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Pokatun.Core.Resources;

namespace Pokatun.Droid.Controls
{
    public sealed class MultilineCountableEditor : FrameLayout
    {
        private TextView _titleLabel;
        private TextView _counterLabel;
        private TextView _dataTextField;

        private int _maxLenght;

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
                SetupCounterText(MaxLenght - value.Length);
            }
        }

        public string Hint
        {
            get { return _dataTextField.Hint; }
            set
            {
                _dataTextField.Hint = value;
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

        public event EventHandler DataChanged;

        public MultilineCountableEditor(Context context) : base(context)
        {
            Initialize(context);
        }

        public MultilineCountableEditor(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(context);
        }

        public MultilineCountableEditor(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Initialize(context);
        }

        private void Initialize(Context context)
        {
            View view = LayoutInflater.From(context).Inflate(Resource.Layout.control_multiline_countable_editor, this);

            _titleLabel = view.FindViewById<TextView>(Resource.Id.titleLabel);
            _counterLabel = view.FindViewById<TextView>(Resource.Id.counterLabel);
            _dataTextField = view.FindViewById<TextView>(Resource.Id.dataTextField);

            _dataTextField.TextChanged += OnDataTextChanged;
        }

        private void OnDataTextChanged(object sender, TextChangedEventArgs e)
        {
            DataChanged?.Invoke(this, e);
            SetupCounterText(MaxLenght - e.Text.Count());
        }

        private void SetupCounterText(int counterVal)
        {
            _counterLabel.Text = string.Format(Strings.SymbolsLeftFormat, counterVal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataTextField.TextChanged -= OnDataTextChanged;
            }

            base.Dispose(disposing);
        }
    }
}
