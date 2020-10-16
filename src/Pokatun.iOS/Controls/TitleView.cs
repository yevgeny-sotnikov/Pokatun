using System;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Pokatun.iOS.Controls
{
    public partial class TitleView : UIView
    {
        public static TitleView Create()
        {
            NSArray nib = NSBundle.MainBundle.LoadNib(nameof(TitleView), null, null);

            TitleView view = Runtime.GetNSObject<TitleView>(nib.ValueAt(0));

            return view;
        }

        public TitleView(IntPtr handle) : base(handle)
        {
        }
    }
}

