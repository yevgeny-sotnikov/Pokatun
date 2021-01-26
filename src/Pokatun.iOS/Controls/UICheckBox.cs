using System;
using System.ComponentModel;
using Foundation;
using UIKit;

namespace Pokatun.iOS.Controls
{
    [DesignTimeVisible(true)]
    [Register(nameof(UICheckBox))]
    public sealed class UICheckBox : UIButton
    {
        public UICheckBox(IntPtr handle) : base(handle)
        {
        }

        public event EventHandler SelectedChanged;

        public override bool Selected
        {
            get { return base.Selected; }
            set
            {
                base.Selected = value;
                SelectedChanged?.Invoke(this, new EventArgs());
            }
        }
    }
}
