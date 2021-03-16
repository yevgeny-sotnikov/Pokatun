using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.Tourist;

namespace Pokatun.Droid.Views.Tourist
{
    [MvxFragmentPresentation(
        typeof(MainContainerViewModel),
        Resource.Id.content_frame,
        AddToBackStack = true,
        EnterAnimation = Android.Resource.Animation.SlideInLeft,
        PopEnterAnimation = Android.Resource.Animation.SlideInLeft,
        ExitAnimation = Android.Resource.Animation.SlideOutRight,
        PopExitAnimation = Android.Resource.Animation.SlideOutRight
    )]
    public class ShowTouristProfileFragment : BaseFragment<ShowTouristProfileViewModel>
    {
        private TextView _fullnameLabel;
        private TextView _phoneLabel;
        private TextView _emailLabel;

        protected override int FragmentLayoutId => Resource.Layout.fragment_show_tourist_profile;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _fullnameLabel = view.FindViewById<TextView>(Resource.Id.fullnameLabel);
            _phoneLabel = view.FindViewById<TextView>(Resource.Id.phoneLabel);
            _emailLabel = view.FindViewById<TextView>(Resource.Id.emailLabel);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(ToolbarTitleLabel).For(v => v.Text).To(vm => vm.Title).OneWay();
            set.Bind(ToolbarPhotoView).For(v => v.ImageStream).To(vm => vm.PhotoStream).OneWay();
            set.Bind(ToolbarRightButton).For(ToolbarRightButton.BindClick()).To(vm => vm.EditCommand).OneTime();
            set.Bind(ToolbarPhotoPlaceholderLabel).To(vm => vm.Placeholder).OneWay();

            set.Bind(_fullnameLabel).To(vm => vm.FullName).OneWay();
            set.Bind(_emailLabel).To(vm => vm.Email).OneWay();
            set.Bind(_phoneLabel).To(vm => vm.Phone).OneWay();

            set.Apply();

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();

            ToolbarLeftSpaceView.Visibility = ViewStates.Gone;
            ToolbarRightButton.SetImageResource(Resource.Drawable.edit);

            ToolbarRightButton.Visibility = ViewStates.Visible;
            ToolbarAddPhotoButton.Visibility = ViewStates.Invisible;
            ToolbarPhotoContainer.Visibility = ViewStates.Visible;
            ToolbarPhotoPlaceholderLabel.Visibility = ViewStates.Visible;
        }

    }
}
