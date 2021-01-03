using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using ObjCRuntime;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Controls
{
    public sealed partial class TitlePhotoView : UIView
    {
        public static TitlePhotoView Create(bool clickable = false)
        {
            NSArray nib = NSBundle.MainBundle.LoadNib(nameof(TitlePhotoView), null, null);

            TitlePhotoView view = Runtime.GetNSObject<TitlePhotoView>(nib.ValueAt(0));

            view._addPhotoButton.Hidden = !clickable;
            view._placeholderLabel.Hidden = clickable;

            return view;
        }

        public string ImagePath
        {
            get { return _imageView.ImagePath; }
            set { _imageView.ImagePath = value; }
        }

        public Func<CancellationToken, Task<Stream>> ImageStream
        {
            get { return _imageView.ImageStream; }
            set { _imageView.ImageStream = value; }
        }

        public string Placeholder
        {
            get { return _placeholderLabel.Text; }
            set { _placeholderLabel.Text = value; }
        }

        public event EventHandler Clicked;


        public TitlePhotoView(IntPtr handle) : base(handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            _placeholderLabel.ApplyTitleLabelStyle();
            _addPhotoButton.ApplyShadowedButtonStyle();
            _addPhotoButton.TouchUpInside += OnTouchUpInside;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _addPhotoButton.TouchUpInside -= OnTouchUpInside;
            }

            base.Dispose(disposing);
        }

        private void OnTouchUpInside(object sender, EventArgs e)
        {
            Clicked?.Invoke(this, new EventArgs());
        }
    }
}

