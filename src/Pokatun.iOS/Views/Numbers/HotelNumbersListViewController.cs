using System;
using System.Collections.Generic;
using Foundation;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Pokatun.Core.ViewModels.Numbers;
using Pokatun.iOS.Cells;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.Numbers
{
    [MvxChildPresentation]
    public sealed partial class HotelNumbersListViewController : TablesViewController<HotelNumbersListViewModel>
    {
        private MvxDeletableSimpleTableViewSource _hotelNumbersTableViewSource;

        public HotelNumbersListViewController() : base(nameof(HotelNumbersListViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            UIBarButtonItem rightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Add);

            NavigationItem.SetRightBarButtonItem(rightBarButtonItem, true);

            _hotelNumbersTableViewSource = (MvxDeletableSimpleTableViewSource)CreateTableViewSource(_tableView, HotelNumberViewCell.Key);
            _tableView.Source = _hotelNumbersTableViewSource;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(NavigationItem.RightBarButtonItem).To(vm => vm.AddCommand).OneTime();
            set.Bind(_hotelNumbersTableViewSource).For(s => s.DeleteRowCommand).To(vm => vm.DeleteCommand);
            set.Bind(_hotelNumbersTableViewSource).To(vm => vm.HotelNumbers).OneTime();
            set.Bind(_hotelNumbersTableViewSource).For(v => v.SelectionChangedCommand).To(vm => vm.OpenHotelNumberCommand).OneTime();


            set.Apply();
        }

        protected override MvxSimpleTableViewSource CreateTableViewSource(UITableView tableView, string nibName)
        {
            return new MvxDeletableSimpleTableViewSource(tableView, nibName)
            {
                UseAnimations = true,
                RemoveAnimation = UITableViewRowAnimation.Right,
                AddAnimation = UITableViewRowAnimation.Fade
            };
        }

        private sealed class MvxDeletableSimpleTableViewSource : MvxSimpleTableViewSource
        {
            public IMvxAsyncCommand<int> DeleteRowCommand { get; set; }

            public MvxDeletableSimpleTableViewSource(UITableView tableView, string nibName) : base(tableView, nibName)
            {
            }

            public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
            {
                return true;
            }

            public override UISwipeActionsConfiguration GetTrailingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
            {
                UIContextualAction deleteAction = UIContextualAction.FromContextualActionStyle(
                    UIContextualActionStyle.Destructive,
                    "",
                    (action, sourceView, completionHandler) => { DeleteRowCommand.Execute(indexPath.Row);  completionHandler(true); }
                );
                deleteAction.Image = UIImage.FromBundle("del_list");
                deleteAction.BackgroundColor = ColorPalette.DeletionColor;

                UISwipeActionsConfiguration conf = UISwipeActionsConfiguration.FromActions(new UIContextualAction[] { deleteAction });
                conf.PerformsFirstActionWithFullSwipe = true;

                return conf;
            }
        }
    }
}

