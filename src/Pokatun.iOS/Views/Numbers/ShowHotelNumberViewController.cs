using System;
using Pokatun.Core.ViewModels.Numbers;
using Pokatun.iOS.Controls;
using UIKit;

namespace Pokatun.iOS.Views.Numbers
{
    public partial class ShowHotelNumberViewController : BaseViewController<ShowHotelNumberViewModel>
    {
        public ShowHotelNumberViewController() : base(nameof(ShowHotelNumberViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            ViewTitle.IsSubtitleHidden = false;

            UIBarButtonItem rightBarButtonItem = new UIBarButtonItem { Image = UIImage.FromBundle("edit") };

            NavigationItem.SetRightBarButtonItem(rightBarButtonItem, true);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Apply();
        }
    }
}

