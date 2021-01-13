using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.Converters;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.Numbers;

namespace Pokatun.Droid.Views.Numbers
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
    public class EditHotelNumberFragment : BaseFragment<EditHotelNumberViewModel>
    {
        private EditText _roomNumberTextField;
        private Button _selectRoomLevelButton;
        private Button _amountOfRoomsButton;
        private Button _amountOfVisitorsButton;
        private EditText _numberDescriptionTextField;
        private TextView _cleaningLabel;
        private SwitchCompat _cleaningNeededSwitch;

        protected override int FragmentLayoutId => Resource.Layout.fragment_edit_hotel_number;

        protected override bool IsHeaderBackButtonVisible => false;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _roomNumberTextField = view.FindViewById<EditText>(Resource.Id.roomNumberTextField);
            _selectRoomLevelButton = view.FindViewById<Button>(Resource.Id.selectRoomLevelButton);
            _amountOfRoomsButton = view.FindViewById<Button>(Resource.Id.amountOfRoomsButton);
            _amountOfVisitorsButton = view.FindViewById<Button>(Resource.Id.amountOfVisitorsButton);
            _numberDescriptionTextField = view.FindViewById<EditText>(Resource.Id.numberDescriptionTextField);
            _cleaningLabel = view.FindViewById<TextView>(Resource.Id.cleaningLabel);
            _cleaningNeededSwitch = view.FindViewById<SwitchCompat>(Resource.Id.cleaningNeededSwitch);

            _roomNumberTextField.Hint = Strings.RoomNumber;
            _numberDescriptionTextField.Hint = Strings.HotelNumberDescription;
            _cleaningLabel.Text = Strings.NumbersCleaning;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(ToolbarRightButton).For(ToolbarRightButton.BindClick()).To(vm => vm.CloseCommand).OneTime();

            set.Bind(_roomNumberTextField).To(vm => vm.Number).TwoWay();
            set.Bind(_numberDescriptionTextField).To(vm => vm.Description).TwoWay();

            set.Bind(_selectRoomLevelButton).For(v => v.Text).To(vm => vm.Level).WithConversion<RoomLevelToStringConverter>().OneWay();
            set.Bind(_amountOfRoomsButton).For(v => v.Text).To(vm => vm.RoomsAmount)
                .WithConversion<StringFormatValueConverter>(Strings.RoomsNumFormat).OneWay();
            set.Bind(_amountOfVisitorsButton).For(v => v.Text).To(vm => vm.VisitorsAmount)
                .WithConversion<StringFormatValueConverter>(Strings.PeopleNumFormat).OneWay();

            set.Bind(_cleaningNeededSwitch).For(_cleaningNeededSwitch.BindChecked()).To(vm => vm.CleaningNeeded).TwoWay();

            set.Bind(_selectRoomLevelButton).To(vm => vm.SelectRoomLevelCommand).OneTime();
            set.Bind(_amountOfRoomsButton).To(vm => vm.PromptRoomsAmountCommand).OneTime();
            set.Bind(_amountOfVisitorsButton).To(vm => vm.PromptVisitorsAmountCommand).OneTime();

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
