using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Pokatun.Droid.Controls
{
    public sealed class MenuItem : FrameLayout
    {
        private ImageView _menuItemIcon;
        private TextView _menuTextView;

        public string Text
        {
            get { return _menuTextView.Text; }
            set { _menuTextView.Text = value; }
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
        }
    }
}
