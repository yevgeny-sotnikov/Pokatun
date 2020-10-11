
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.ViewModels.ChoiseUserRole;
using Pokatun.Core.ViewModels.Main;

namespace Pokatun.Droid.Views.ChoiseUserRole
{
    [MvxFragmentPresentation(typeof(MainContainerViewModel), Resource.Id.content_frame)]
    public class ChoiseUserRoleFragment : BaseFragment<ChoiseUserRoleViewModel>
    {
        protected override int FragmentLayoutId => Resource.Layout.fragment_choise_user_role;
    }
}
