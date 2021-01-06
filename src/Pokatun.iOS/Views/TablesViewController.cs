using System;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using Pokatun.Core.ViewModels;
using UIKit;

namespace Pokatun.iOS.Views
{
    public abstract class TablesViewController<TViewModel> : BaseViewController<TViewModel> where TViewModel : BaseViewModel
    {
        protected TablesViewController(string name, NSBundle bundle) : base(name, bundle)
        {
        }

        protected MvxSimpleTableViewSource CreateTableViewSource(UITableView tableView, string nibName)
        {
            return new MvxSimpleTableViewSource(tableView, nibName)
            {
                UseAnimations = true,
                RemoveAnimation = UITableViewRowAnimation.Right,
                AddAnimation = UITableViewRowAnimation.Fade
            };
        }

    }
}
