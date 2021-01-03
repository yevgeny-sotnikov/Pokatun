using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views.Fragments;
using Pokatun.Droid.Views.Main;
using Pokatun.Core.ViewModels;
using FFImageLoading.Cross;

namespace Pokatun.Droid.Views
{
    public abstract class BaseFragment<TViewModel> : MvxFragment<TViewModel> where TViewModel : BaseViewModel
    {
        protected abstract int FragmentLayoutId { get; }

        private MainContainerActivity CompatActivity => (MainContainerActivity)Activity;

        private ImageView ToolbarLogo => CompatActivity.ToolbarLogo;

        private TextView ToolbarTitleLabel => CompatActivity.ToolbarTitleLabel;

        protected ImageButton ToolbarAddPhotoButton => CompatActivity.ToolbarAddPhotoButton;

        protected ImageButton ToolbarRightButton => CompatActivity.ToolbarRightButton;

        protected MvxCachedImageView ToolbarPhotoView => CompatActivity.ToolbarPhotoView;

        protected TextView ToolbarPhotoPlaceholderLabel => CompatActivity.ToolbarPhotoPlaceholderLabel;

        protected FrameLayout ToolbarPhotoContainer => CompatActivity.ToolbarPhotoContainer;

        protected View ToolbarLeftSpaceView => CompatActivity.ToolbarLeftSpaceView;

        protected virtual bool IsHeaderBackButtonVisible => true;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            return this.BindingInflate(FragmentLayoutId, container, false);
        }

        public override void OnStart()
        {
            base.OnStart();

            CompatActivity.SupportActionBar.SetDisplayHomeAsUpEnabled(IsHeaderBackButtonVisible);
            CompatActivity.SupportActionBar.SetDisplayShowHomeEnabled(IsHeaderBackButtonVisible);

            string title = ViewModel.Title;
            bool titleNotExists = string.IsNullOrWhiteSpace(title);
            
            ToolbarLogo.Visibility = titleNotExists ? ViewStates.Visible : ViewStates.Gone;
            ToolbarTitleLabel.Visibility = titleNotExists ? ViewStates.Gone : ViewStates.Visible;
            ToolbarLeftSpaceView.Visibility = ViewStates.Gone;
            ToolbarRightButton.Visibility = ViewStates.Gone;
            ToolbarPhotoPlaceholderLabel.Visibility = ViewStates.Gone;
            ToolbarAddPhotoButton.Visibility = ViewStates.Gone;
            ToolbarPhotoContainer.Visibility = ViewStates.Gone;

            ToolbarTitleLabel.Text = title;
        }
    }
}
