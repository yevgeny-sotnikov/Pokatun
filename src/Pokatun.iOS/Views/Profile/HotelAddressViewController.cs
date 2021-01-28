using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.ViewModels.Profile;
using UIKit;

namespace Pokatun.iOS.Views.Profile
{
    [MvxModalPresentation(WrapInNavigationController = true, ModalPresentationStyle = UIModalPresentationStyle.BlurOverFullScreen)]
    public partial class HotelAddressViewController : BaseViewController<HotelAddressViewModel>
    {
        public HotelAddressViewController() : base(nameof(HotelAddressViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            UIBarButtonItem rightBarButtonItem = new UIBarButtonItem { Image = UIImage.FromBundle("close") };
            NavigationItem.SetRightBarButtonItem(rightBarButtonItem, true);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(NavigationItem.RightBarButtonItem).To(vm => vm.CloseCommand).OneTime();

            set.Apply();
        }
    }
}

