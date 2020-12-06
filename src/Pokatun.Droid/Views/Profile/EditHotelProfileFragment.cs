using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views.Fragments;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.Profile;
using static Android.Widget.TabHost;

namespace Pokatun.Droid.Views.Profile
{
    [MvxFragmentPresentation(
        typeof(MainContainerViewModel),
        Resource.Id.content_frame,
        AddToBackStack = true,
        EnterAnimation = Android.Resource.Animation.FadeIn,
        PopEnterAnimation = Android.Resource.Animation.FadeIn,
        ExitAnimation = Android.Resource.Animation.FadeOut,
        PopExitAnimation = Android.Resource.Animation.FadeOut
    )]
    public sealed class EditHotelProfileFragment : BaseFragment<EditHotelProfileViewModel>
    {
        private TabHost _tabHost;

        protected override int FragmentLayoutId => Resource.Layout.fragment_edit_hotel_profile;

        protected override bool IsHeaderBackButtonVisible => false;
        

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _tabHost = view.FindViewById<TabHost>(Android.Resource.Id.TabHost);

            _tabHost.Setup();

            TabSpec tabSpec = _tabHost.NewTabSpec("personalData");
            tabSpec.SetContent(Resource.Id.personalDataTab);
            tabSpec.SetIndicator(LayoutInflater.Inflate(Resource.Layout.personal_data_tab, null));
            _tabHost.AddTab(tabSpec);

            tabSpec = _tabHost.NewTabSpec("hotelInfo");
            tabSpec.SetContent(Resource.Id.hotelInfoTab);
            tabSpec.SetIndicator(LayoutInflater.Inflate(Resource.Layout.hotel_info_tab, null));
            _tabHost.AddTab(tabSpec);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(ToolbarRightButton).For(ToolbarRightButton.BindClick()).To(vm => vm.CloseCommand);

            set.Apply();

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();

            ToolbarRightButton.SetImageResource(Resource.Drawable.close);
            ToolbarRightButton.Visibility = ViewStates.Visible;

        }
    }
}
