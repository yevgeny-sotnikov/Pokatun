using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.ViewModels.ChoiseUserRole;
using UIKit;

namespace Pokatun.iOS.Views.ChoiseUserRole
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class ChoiseUserRoleViewController : BaseViewController<ChoiseUserRoleViewModel>
    {
        public ChoiseUserRoleViewController() : base(nameof(ChoiseUserRoleViewController), null)
        {
        }
    }
}

