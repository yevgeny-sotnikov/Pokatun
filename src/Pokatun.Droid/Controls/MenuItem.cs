using System.Text.RegularExpressions;
using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;

namespace Pokatun.Droid.Controls
{
    public sealed class MenuItem : FrameLayout
    {
        private ImageView _menuItemIcon;
        private TextView _menuTextView;
        private TextView _additionalInfoLabel;

        public string Text
        {
            get { return _menuTextView.Text; }
            set { _menuTextView.Text = value; }
        }

        public string AdditionalInfo
        {
            get { return _additionalInfoLabel.Text; }
            set
            {
                value = value ?? string.Empty;

                SpannableString spannableString = new SpannableString(value);

                foreach (Match m in Regex.Matches(value, Core.Constants.CirclePattern))
                {
                    spannableString.SetSpan(new ForegroundColorSpan(
                        new Color(ContextCompat.GetColor(Context, Resource.Color.faileValidationColor))),
                        m.Index,
                        m.Index + m.Length,
                        SpanTypes.ExclusiveExclusive
                    );
                }

                _additionalInfoLabel.SetText(spannableString, TextView.BufferType.Spannable);
            }
        }

        public ViewStates AdditionalInfoVisibility
        {
            get { return _additionalInfoLabel.Visibility; }
            set
            {
                _additionalInfoLabel.Visibility = value;
            }
        }


        public void SetImageResource(int resId)
        {
            _menuItemIcon.SetImageResource(resId);
        }

        public MenuItem(Context context) : base(context)
        {
            Initialize(context);
        }

        public MenuItem(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(context);
        }

        public MenuItem(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Initialize(context);
        }

        private void Initialize(Context context)
        {
            View view = LayoutInflater.From(context).Inflate(Resource.Layout.control_menu_item, this);

            _menuItemIcon = view.FindViewById<ImageView>(Resource.Id.menuItemIcon);
            _menuTextView = view.FindViewById<TextView>(Resource.Id.menuTextView);
            _additionalInfoLabel = view.FindViewById<TextView>(Resource.Id.additionalInfoLabel);
        }
    }
}
