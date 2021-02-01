using System;
using Pokatun.Core.Converters;
using Pokatun.Core.Resources;
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

            set.Bind(_levelLabel).To(vm => vm.HotelNumber.Level).WithConversion<RoomLevelToStringConverter>().OneWay();
            set.Bind(_roomsAmountLabel).To(vm => vm.HotelNumber.RoomsAmount).WithConversion<StringFormatValueConverter>(Strings.RoomsCounter).OneWay();
            set.Bind(_visitorsAmountLabel).To(vm => vm.HotelNumber.VisitorsAmount).WithConversion<StringFormatValueConverter>(Strings.VisitorsCounter).OneWay();

            #pragma warning restore IDE0008 // Use explicit type

            set.Apply();
        }
    }
}

