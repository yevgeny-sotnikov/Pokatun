using System;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Pokatun.Core.ViewModels.Numbers;
using Pokatun.iOS.Cells;
using UIKit;

namespace Pokatun.iOS.Views.Numbers
{
    [MvxChildPresentation]
    public sealed partial class HotelNumbersListViewController : TablesViewController<HotelNumbersListViewModel>
    {
        private MvxSimpleTableViewSource _hotelNumbersTableViewSource;

        public HotelNumbersListViewController() : base(nameof(HotelNumbersListViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            UIBarButtonItem rightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Add);

            NavigationItem.SetRightBarButtonItem(rightBarButtonItem, true);

            _hotelNumbersTableViewSource = CreateTableViewSource(_tableView, HotelNumberViewCell.Key);
            _tableView.Source = _hotelNumbersTableViewSource;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(NavigationItem.RightBarButtonItem).To(vm => vm.AddCommand).OneTime();
            set.Bind(_hotelNumbersTableViewSource).To(vm => vm.HotelNumbers).OneTime();

            set.Apply();
        }
    }
}

