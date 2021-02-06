using Android.OS;
using Android.Views;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.Menu;
using Pokatun.Droid.Controls;

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
    public class TouristMenuFragment : BaseFragment<TouristMenuViewModel>
    {
        private MenuItem _activeBidItem;
        private MenuItem _bidsArchiveItem;
        private MenuItem _myRatesItem;
        private MenuItem _profileItem;
        private MenuItem _paymentItem;
        private MenuItem _securityItem;
        private MenuItem _exitItem;

        protected override int FragmentLayoutId => Resource.Layout.fragment_tourist_menu;

        protected override bool IsHeaderBackButtonVisible => false;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _activeBidItem = view.FindViewById<MenuItem>(Resource.Id.activeBidItem);
            _bidsArchiveItem = view.FindViewById<MenuItem>(Resource.Id.bidsArchiveItem);
            _myRatesItem = view.FindViewById<MenuItem>(Resource.Id.myRatesItem);
            _profileItem = view.FindViewById<MenuItem>(Resource.Id.profileItem);
            _paymentItem = view.FindViewById<MenuItem>(Resource.Id.paymentItem);
            _securityItem = view.FindViewById<MenuItem>(Resource.Id.securityItem);
            _exitItem = view.FindViewById<MenuItem>(Resource.Id.exitItem);

            ToolbarPhotoPlaceholderLabel.Text = ViewModel.Placeholder;

            _activeBidItem.Text = Strings.MyBids;
            _bidsArchiveItem.Text = Strings.BidsArchive;
            _myRatesItem.Text = Strings.MyRates;
            _profileItem.Text = Strings.Profile;
            _paymentItem.Text = Strings.Payment;
            _securityItem.Text = Strings.Security;
            _exitItem.Text = Strings.Exit;

            _activeBidItem.SetImageResource(Resource.Drawable.active);
            _bidsArchiveItem.SetImageResource(Resource.Drawable.folder);
            _myRatesItem.SetImageResource(Resource.Drawable.like);
            _profileItem.SetImageResource(Resource.Drawable.smile);
            _paymentItem.SetImageResource(Resource.Drawable.pay);
            _securityItem.SetImageResource(Resource.Drawable.password);
            _exitItem.SetImageResource(Resource.Drawable.logout);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(ToolbarPhotoView).For(v => v.ImageStream).To(vm => vm.PhotoStream).OneWay();

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
        }

    }
}
