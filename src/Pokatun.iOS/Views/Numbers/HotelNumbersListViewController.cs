using System;
using Foundation;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Pokatun.Core.ViewModels.Numbers;
using UIKit;

namespace Pokatun.iOS.Views.Numbers
{
    [MvxChildPresentation]
    public sealed partial class HotelNumbersListViewController : TablesViewController<HotelNumbersListViewModel>
    {
        public HotelNumbersListViewController() : base(nameof(HotelNumbersListViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            UIBarButtonItem rightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Add);

            NavigationItem.SetRightBarButtonItem(rightBarButtonItem, true);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(NavigationItem.RightBarButtonItem).To(vm => vm.AddCommand).OneTime();

            set.Apply();
        }
    }
}

