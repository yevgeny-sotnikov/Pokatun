using System;
using System.ComponentModel;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
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

        [DisplayName(nameof(Text)), Export("text"), Browsable(true)]
        public string Text
        {
            get { return _menuTextView?.Text; }
            set
            {
                _text = value;

                if (_menuTextView == null) return;

                _menuTextView.Text = value;
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

            NSBundle.MainBundle.LoadNib(nameof(MenuItem), this, null);
            AddSubview(_rootView);

            Image = _image;
            Text = _text;

            _rootView.TranslatesAutoresizingMaskIntoConstraints = false;
            _rootView.TopAnchor.ConstraintEqualTo(TopAnchor, 0).Active = true;
            _rootView.LeftAnchor.ConstraintEqualTo(LeftAnchor, 0).Active = true;
            _rootView.RightAnchor.ConstraintEqualTo(RightAnchor, 0).Active = true;
            _rootView.BottomAnchor.ConstraintEqualTo(BottomAnchor, 0).Active = true;

            _menuTextView.ApplyLargeLabelStyle();

            AddGestureRecognizer(new UITapGestureRecognizer(OnItemTapped));
        }

        private async void OnItemTapped()
        {
            await AnimateAsync(ClickAnimationDuration, () => { Alpha = 0.5f; BackgroundColor = UIColor.SystemFillColor; });
            await AnimateAsync(ClickAnimationDuration, () => { Alpha = 1f; BackgroundColor = UIColor.Clear; });
            Clicked?.Invoke(this, new EventArgs());
        }
    }
}

