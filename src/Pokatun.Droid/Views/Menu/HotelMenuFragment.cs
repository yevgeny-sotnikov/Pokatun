using Android.OS;
using Android.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.Menu;
using Pokatun.Droid.Controls;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Plugin.Visibility;

namespace Pokatun.Droid.Views.Menu
{
    [MvxFragmentPresentation(
        typeof(MainContainerViewModel),
        Resource.Id.content_frame,
        AddToBackStack = false,
        PopEnterAnimation = Android.Resource.Animation.FadeIn,
        ExitAnimation = Android.Resource.Animation.SlideOutRight,
        PopExitAnimation = Android.Resource.Animation.SlideOutRight
    )]
    public class HotelMenuFragment : BaseFragment<HotelMenuViewModel>
    {
        private MenuItem _myBidsItem;
        private MenuItem _myHotelNumbersItem;
        private MenuItem _hotelRatingItem;
        private MenuItem _profileItem;
        private MenuItem _conditionsAndLoyaltyProgramItem;
        private MenuItem _securityItem;
        private MenuItem _exitItem;

        protected override int FragmentLayoutId => Resource.Layout.fragment_hotel_menu;

        protected override bool IsHeaderBackButtonVisible => false;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _myBidsItem = view.FindViewById<MenuItem>(Resource.Id.myBidsItem);
            _myHotelNumbersItem = view.FindViewById<MenuItem>(Resource.Id.myHotelNumbersItem);
            _hotelRatingItem = view.FindViewById<MenuItem>(Resource.Id.hotelRatingItem);
            _profileItem = view.FindViewById<MenuItem>(Resource.Id.profileItem);
            _conditionsAndLoyaltyProgramItem = view.FindViewById<MenuItem>(Resource.Id.conditionsAndLoyaltyProgramItem);
            _securityItem = view.FindViewById<MenuItem>(Resource.Id.securityItem);
            _exitItem = view.FindViewById<MenuItem>(Resource.Id.exitItem);

            _myBidsItem.Text = Strings.MyBids;
            _myHotelNumbersItem.Text = Strings.MyHotelNumbers;
            _hotelRatingItem.Text = Strings.HotelRating;
            _profileItem.Text = Strings.Profile;
            _profileItem.AdditionalInfo = Strings.CompleteYourProfile + "  ⬤";
            _conditionsAndLoyaltyProgramItem.Text = Strings.ConditionsAndLoyaltyProgram;
            _securityItem.Text = Strings.Security;
            _exitItem.Text = Strings.Exit;

            _myBidsItem.SetImageResource(Resource.Drawable.applic);
            _myHotelNumbersItem.SetImageResource(Resource.Drawable.room);
            _hotelRatingItem.SetImageResource(Resource.Drawable.rating);
            _profileItem.SetImageResource(Resource.Drawable.profile);
            _conditionsAndLoyaltyProgramItem.SetImageResource(Resource.Drawable.loyaty);
            _securityItem.SetImageResource(Resource.Drawable.password);
            _exitItem.SetImageResource(Resource.Drawable.logout);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(ToolbarPhotoView).For(v => v.ImageStream).To(vm => vm.PhotoStream).OneWay();
            set.Bind(ToolbarPhotoPlaceholderLabel).To(vm => vm.Placeholder).OneWay();
            set.Bind(ToolbarSubtitleLabel).To(vm => vm.Subtitle).OneWay();
            set.Bind(ToolbarSubtitleLabel).For(v => v.Activated).To(vm => vm.ProfileNotCompleted).OneWay();
            set.Bind(_profileItem).For(v => v.AdditionalInfoVisibility).To(vm => vm.ProfileNotCompleted)
                .WithConversion<MvxVisibilityValueConverter>().OneWay();

            set.Bind(_myHotelNumbersItem).For(_myHotelNumbersItem.BindClick()).To(vm => vm.HotelNumbersCommand);
            set.Bind(_profileItem).For(_profileItem.BindClick()).To(vm => vm.ProfileCommand);
            set.Bind(_exitItem).For(_exitItem.BindClick()).To(vm => vm.ExitCommand);

            set.Apply();

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();

            ToolbarLeftSpaceView.Visibility = ViewStates.Visible;
            ToolbarPhotoPlaceholderLabel.Visibility = ViewStates.Visible;
            ToolbarPhotoContainer.Visibility = ViewStates.Visible;
            ToolbarSubtitleLabel.Visibility = ViewStates.Visible;
        }
    }
}