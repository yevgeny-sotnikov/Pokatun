using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.ViewModels;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views.Fragments;
using Pokatun.Droid.Views.Main;
using Pokatun.Core.ViewModels;

namespace Pokatun.Droid.Views
{
    public abstract class BaseFragment<TViewModel> : MvxFragment<TViewModel>
        where TViewModel : BaseViewModel
    {
        protected abstract int FragmentLayoutId { get; }

        private ImageView ToolbarLogo => ((MainContainerActivity)Activity).ToolbarLogo;

        private TextView ToolbarTitleLabel => ((MainContainerActivity)Activity).ToolbarTitleLabel;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            View view = this.BindingInflate(FragmentLayoutId, container, false);

            SetupToolbar();

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();
            SetupToolbar();
        }

        private void SetupToolbar()
        {
            string title = ViewModel.Title;
            bool titleNotExists = string.IsNullOrWhiteSpace(title);

            ToolbarLogo.Visibility = titleNotExists ? ViewStates.Visible : ViewStates.Gone;
            ToolbarTitleLabel.Visibility = titleNotExists ? ViewStates.Gone : ViewStates.Visible;

            ToolbarTitleLabel.Text = title;
        }
    }
}
