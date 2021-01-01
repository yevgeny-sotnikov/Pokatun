using System;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Pokatun.iOS.Controls
{
    [DesignTimeVisible(true), Register(nameof(ResizableTableView))]
    public class ResizableTableView : UITableView
    {
        public ResizableTableView(IntPtr intPtr) : base(intPtr)
        {
        }

        public override CGSize ContentSize
        {
            get
            {
                return base.ContentSize;
            }
            set
            {
                base.ContentSize = value;

                InvalidateIntrinsicContentSize();
            }
        }

        public override CGSize IntrinsicContentSize
        {
            get
            {
                LayoutIfNeeded();

                return new CGSize(UIView.NoIntrinsicMetric, ContentSize.Height);
            }
        }
    }
}
