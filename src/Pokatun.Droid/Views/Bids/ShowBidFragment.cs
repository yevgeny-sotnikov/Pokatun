using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.Converters;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Bids;
using Pokatun.Core.ViewModels.Main;

namespace Pokatun.Droid.Views.Bids
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
    public sealed class ShowBidFragment : BaseFragment<ShowBidViewModel>
    {
        private TextView _timeRangesLabel;
        private TextView _hotelLocationLabel;
        private TextView _levelLabel;
        private TextView _roomsAmountLabel;
        private TextView _visitorsAmountLabel;
        private TextView _inNumberLabel;
        private TextView _descriptionLabel;
        private TextView _cleaningLabel;
        private TextView _cleaningNeededLabel;
        private TextView _nutritionLabel;
        private TextView _nutritionInfoLabel;
        private TextView _checkinLabel;
        private TextView _checkinTimeLabel;
        private TextView _checkoutLabel;
        private TextView _checkoutTimeLabel;
        private TextView _priceLabel;
        private TextView _discountLabel;
        private TextView _priceWithDiscountLabel;
        private TextView _currencyInfoLabel;

        protected override int FragmentLayoutId => Resource.Layout.fragment_show_bid;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _timeRangesLabel = view.FindViewById<TextView>(Resource.Id.timeRangesLabel);
            _hotelLocationLabel = view.FindViewById<TextView>(Resource.Id.hotelLocationLabel);
            _levelLabel = view.FindViewById<TextView>(Resource.Id.levelLabel);
            _roomsAmountLabel = view.FindViewById<TextView>(Resource.Id.roomsAmountLabel);
            _visitorsAmountLabel = view.FindViewById<TextView>(Resource.Id.visitorsAmountLabel);
            _inNumberLabel = view.FindViewById<TextView>(Resource.Id.inNumberLabel);
            _descriptionLabel = view.FindViewById<TextView>(Resource.Id.descriptionLabel);
            _cleaningLabel = view.FindViewById<TextView>(Resource.Id.cleaningLabel);
            _cleaningNeededLabel = view.FindViewById<TextView>(Resource.Id.cleaningNeededLabel);
            _nutritionLabel = view.FindViewById<TextView>(Resource.Id.nutritionLabel);
            _nutritionInfoLabel = view.FindViewById<TextView>(Resource.Id.nutritionInfoLabel);
            _checkinLabel = view.FindViewById<TextView>(Resource.Id.checkinLabel);
            _checkinTimeLabel = view.FindViewById<TextView>(Resource.Id.checkinTimeLabel);
            _checkoutLabel = view.FindViewById<TextView>(Resource.Id.checkoutLabel);
            _checkoutTimeLabel = view.FindViewById<TextView>(Resource.Id.checkoutTimeLabel);
            _priceLabel = view.FindViewById<TextView>(Resource.Id.priceLabel);
            _discountLabel = view.FindViewById<TextView>(Resource.Id.discountLabel);
            _priceWithDiscountLabel = view.FindViewById<TextView>(Resource.Id.priceWithDiscountLabel);
            _currencyInfoLabel = view.FindViewById<TextView>(Resource.Id.currencyInfoLabel);

            _inNumberLabel.Text = Strings.InHotelNumber;
            _cleaningLabel.Text = Strings.NumbersCleaning;
            _nutritionLabel.Text = Strings.Nutrition;
            _checkinLabel.Text = Strings.CheckIn;
            _checkoutLabel.Text = Strings.CheckOut;
            _currencyInfoLabel.Text = Strings.PriceHint;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(ToolbarRightButton).For(ToolbarRightButton.BindClick()).To(vm => vm.EditCommand).OneTime();


            set.Bind(ToolbarSubtitleLabel).To(vm => vm.Subtitle).OneWay();
            set.Bind(_timeRangesLabel).To(vm => vm.TimeRange).OneWay();
            set.Bind(_hotelLocationLabel).To(vm => vm.HotelInfo.Address).OneWay();
            set.Bind(_levelLabel).To(vm => vm.HotelNumber.Level).WithConversion<RoomLevelToStringConverter>().OneWay();
            set.Bind(_roomsAmountLabel).To(vm => vm.HotelNumber.RoomsAmount).WithConversion<StringFormatValueConverter>(Strings.RoomsCounter).OneWay();
            set.Bind(_visitorsAmountLabel).To(vm => vm.HotelNumber.VisitorsAmount).WithConversion<StringFormatValueConverter>(Strings.VisitorsCounter).OneWay();
            set.Bind(_descriptionLabel).To(vm => vm.HotelNumber.Description).OneWay();
            set.Bind(_cleaningNeededLabel).To(vm => vm.HotelNumber.CleaningNeeded).WithDictionaryConversion(new Dictionary<bool, string>
            {
                { true, Strings.Yes },
                { false, Strings.No }
            }).OneWay();
            set.Bind(_nutritionInfoLabel).To(vm => vm.NutritionInfo).OneWay();
            set.Bind(_checkinTimeLabel).To(vm => vm.HotelInfo.CheckInTime).WithConversion<TimeConverter>(Strings.NA).OneWay();
            set.Bind(_checkoutTimeLabel).To(vm => vm.HotelInfo.CheckOutTime).WithConversion<TimeConverter>(Strings.NA).OneWay();
            set.Bind(_priceLabel).To(vm => vm.Bid.Price).WithConversion<StringFormatValueConverter>("{0} " + Strings.PriceHint).OneWay();
            set.Bind(_discountLabel).To(vm => vm.Bid.Discount).WithConversion<StringFormatValueConverter>("-{0}%").OneWay();
            set.Bind(_priceWithDiscountLabel).To(vm => vm.PriceWithDiscount).OneWay();

            set.Apply();

            return view;

        }

        public override void OnStart()
        {
            base.OnStart();

            ToolbarLeftSpaceView.Visibility = ViewStates.Gone;
            ToolbarRightButton.SetImageResource(Resource.Drawable.edit);

            ToolbarSubtitleLabel.Visibility = ViewStates.Visible;
            ToolbarRightButton.Visibility = ViewStates.Visible;
        }

    }
}
