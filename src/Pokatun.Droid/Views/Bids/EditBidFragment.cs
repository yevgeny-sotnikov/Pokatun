
using System;
using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.Views;
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
        EnterAnimation = Android.Resource.Animation.FadeIn,
        PopEnterAnimation = Android.Resource.Animation.FadeIn,
        ExitAnimation = Android.Resource.Animation.FadeOut,
        PopExitAnimation = Android.Resource.Animation.FadeOut
    )]
    public sealed class EditBidFragment : BaseFragment<EditBidViewModel>
    {
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
        private TextView _priceTextField;
        private TextView _discountTextField;
        private MvxLinearLayout _bidTimeRangesTable;
        private Button _addTimeRangeButton;
        private Button _saveChangesButton;

        protected override int FragmentLayoutId => Resource.Layout.fragment_edit_bid;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

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
            _priceTextField = view.FindViewById<TextView>(Resource.Id.priceTextField);
            _discountTextField = view.FindViewById<TextView>(Resource.Id.discountTextField);
            _bidTimeRangesTable = view.FindViewById<MvxLinearLayout>(Resource.Id.bidTimeRangesTable);
            _addTimeRangeButton = view.FindViewById<Button>(Resource.Id.addTimeRangeButton);
            _saveChangesButton = view.FindViewById<Button>(Resource.Id.saveChangesButton);

            _bidTimeRangesTable.ItemTemplateId = Resource.Layout.bid_time_range_item_template;

            _inNumberLabel.Text = Strings.InHotelNumber;
            _cleaningLabel.Text = Strings.NumbersCleaning;
            _nutritionLabel.Text = Strings.Nutrition;
            _checkinLabel.Text = Strings.CheckIn;
            _checkoutLabel.Text = Strings.CheckOut;
            _priceTextField.Hint = Strings.HotelNumberPriceHint;
            _discountTextField.Hint = Strings.DiscountHint;
            _addTimeRangeButton.Text = Strings.AddDate;
            _saveChangesButton.Text = Strings.SaveChanges;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

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
            set.Bind(_priceTextField).To(vm => vm.Price).WithConversion<NullableValueConverter>().TwoWay();
            set.Bind(_discountTextField).To(vm => vm.Discount).WithConversion<NullableValueConverter>().TwoWay();
            set.Bind(_bidTimeRangesTable).For(v => v.ItemsSource).To(vm => vm.TimeRanges).OneTime();

            set.Bind(_priceTextField).For(v => v.Activated).To(vm => vm.IsPriceInvalid).OneWay();
            set.Bind(_discountTextField).For(v => v.Activated).To(vm => vm.IsDiscountInvalid).OneWay();

            set.Bind(_addTimeRangeButton).To(vm => vm.AddTimeRangeCommand).OneTime();
            set.Bind(_saveChangesButton).To(vm => vm.SaveChangesCommand).OneTime();

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
