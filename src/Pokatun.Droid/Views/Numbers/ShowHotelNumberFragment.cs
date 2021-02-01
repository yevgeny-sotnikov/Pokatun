using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.Converters;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.Numbers;
using MvvmCross.Binding.BindingContext;

namespace Pokatun.Droid.Views.Numbers
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
    public sealed class ShowHotelNumberFragment : BaseFragment<ShowHotelNumberViewModel>
    {
        private TextView _levelLabel;
        private TextView _roomsAmountLabel;
        private TextView _visitorsAmountLabel;
        private TextView _inNumberLabel;
        private TextView _descriptionLabel;
        private TextView _cleaningLabel;
        private TextView _cleaningNeededLabel;

        protected override int FragmentLayoutId => Resource.Layout.fragment_show_hotel_number;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _levelLabel = view.FindViewById<TextView>(Resource.Id.levelLabel);
            _roomsAmountLabel = view.FindViewById<TextView>(Resource.Id.roomsAmountLabel);
            _visitorsAmountLabel = view.FindViewById<TextView>(Resource.Id.visitorsAmountLabel);
            _inNumberLabel = view.FindViewById<TextView>(Resource.Id.inNumberLabel);
            _descriptionLabel = view.FindViewById<TextView>(Resource.Id.descriptionLabel);
            _cleaningLabel = view.FindViewById<TextView>(Resource.Id.cleaningLabel);
            _cleaningNeededLabel = view.FindViewById<TextView>(Resource.Id.cleaningNeededLabel);

            _inNumberLabel.Text = Strings.InHotelNumber;
            _cleaningLabel.Text = Strings.NumbersCleaning;
#pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(ToolbarSubtitleLabel).To(vm => vm.Subtitle).OneWay();

            set.Bind(_levelLabel).To(vm => vm.HotelNumber.Level).WithConversion<RoomLevelToStringConverter>().OneWay();
            set.Bind(_roomsAmountLabel).To(vm => vm.HotelNumber.RoomsAmount).WithConversion<StringFormatValueConverter>(Strings.RoomsCounter).OneWay();
            set.Bind(_visitorsAmountLabel).To(vm => vm.HotelNumber.VisitorsAmount).WithConversion<StringFormatValueConverter>(Strings.VisitorsCounter).OneWay();
            set.Bind(_descriptionLabel).To(vm => vm.HotelNumber.Description).OneWay();
            set.Bind(_cleaningNeededLabel).To(vm => vm.HotelNumber.CleaningNeeded).WithDictionaryConversion(new Dictionary<bool, string>
            {
                { true, "Да" },
                { false, "Нет" }
            }).OneWay();

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
            ToolbarPhotoPlaceholderLabel.Visibility = ViewStates.Visible;
        }
    }
}
