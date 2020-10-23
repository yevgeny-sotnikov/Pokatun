using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views.Fragments;
using Pokatun.Droid.Views.Main;
using Pokatun.Core.ViewModels;
using Android.App;

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

            return this.BindingInflate(FragmentLayoutId, container, false);
        }

        public override void OnStart()
        {
            base.OnStart();

            string title = ViewModel.Title;
            bool titleNotExists = string.IsNullOrWhiteSpace(title);

            ToolbarLogo.Visibility = titleNotExists ? ViewStates.Visible : ViewStates.Gone;
            ToolbarTitleLabel.Visibility = titleNotExists ? ViewStates.Gone : ViewStates.Visible;

            ToolbarTitleLabel.Text = title;
        }
    }
}
