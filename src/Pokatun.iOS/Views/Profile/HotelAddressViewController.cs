using System;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.ViewModels.Profile;
using Pokatun.iOS.Cells;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.Profile
{
    [MvxModalPresentation(WrapInNavigationController = true, ModalPresentationStyle = UIModalPresentationStyle.BlurOverFullScreen)]
    public partial class HotelAddressViewController : TablesViewController<HotelAddressViewModel>
    {
        private MvxSimpleTableViewSource _foundResultsTableViewSource;

        public HotelAddressViewController() : base(nameof(HotelAddressViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            UIBarButtonItem rightBarButtonItem = new UIBarButtonItem { Image = UIImage.FromBundle("close") };
            NavigationItem.SetRightBarButtonItem(rightBarButtonItem, true);

            _searchTextField.ApplyBorderedEditTextStyle();

            _foundResultsTableViewSource = CreateTableViewSource(_foundResultsTable, AddressItemViewCell.Key);

            _foundResultsTable.Source = _foundResultsTableViewSource;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(NavigationItem.RightBarButtonItem).To(vm => vm.CloseCommand).OneTime();
            set.Bind(_searchTextField).For(v => v.Text).To(vm => vm.SearchText).OneWayToSource();
            set.Bind(_foundResultsTableViewSource).To(vm => vm.FoundAdresses).OneTime();
            set.Bind(_foundResultsTableViewSource).For(v => v.SelectionChangedCommand).To(vm => vm.AddressSelectedCommand);

            set.Apply();
        }
    }
}

