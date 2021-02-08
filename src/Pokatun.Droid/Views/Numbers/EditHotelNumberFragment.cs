using Android.OS;
using Android.Views;
using Android.Widget;
using Google.Android.Material.SwitchMaterial;
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
        private TextView _nutritionLabel;
        private SwitchMaterial _cleaningNeededSwitch;
        private SwitchMaterial _nutritionNeededSwitch;
        private CheckBox _breakfastCheckbox;
        private CheckBox _dinnerCheckbox;
        private CheckBox _supperCheckbox;
        private Button _saveChangesButton;

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
            _nutritionLabel = view.FindViewById<TextView>(Resource.Id.nutritionLabel);
            _cleaningNeededSwitch = view.FindViewById<SwitchMaterial>(Resource.Id.cleaningNeededSwitch);
            _nutritionNeededSwitch = view.FindViewById<SwitchMaterial>(Resource.Id.nutritionNeededSwitch);
            _breakfastCheckbox = view.FindViewById<CheckBox>(Resource.Id.breakfastCheckbox);
            _dinnerCheckbox = view.FindViewById<CheckBox>(Resource.Id.dinnerCheckbox);
            _supperCheckbox = view.FindViewById<CheckBox>(Resource.Id.supperCheckbox);
            _saveChangesButton = view.FindViewById<Button>(Resource.Id.saveChangesButton);

            _roomNumberTextField.Hint = Strings.RoomNumber;
            _numberDescriptionTextField.Hint = Strings.HotelNumberDescription;
            _cleaningLabel.Text = Strings.NumbersCleaning;
            _nutritionLabel.Text = Strings.Nutrition;
            _breakfastCheckbox.Text = Strings.Breakfast;
            _dinnerCheckbox.Text = Strings.Dinner;
            _supperCheckbox.Text = Strings.Supper;
            _saveChangesButton.Text = Strings.SaveChanges;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(ToolbarRightButton).For(ToolbarRightButton.BindClick()).To(vm => vm.CloseCommand).OneTime();

            set.Bind(_roomNumberTextField).To(vm => vm.Number).WithConversion<NullableValueConverter>().TwoWay();
            set.Bind(_numberDescriptionTextField).To(vm => vm.Description).TwoWay();

            set.Bind(_selectRoomLevelButton).For(v => v.Text).To(vm => vm.Level).WithConversion<RoomLevelToStringConverter>().OneWay();
            set.Bind(_amountOfRoomsButton).For(v => v.Text).To(vm => vm.RoomsAmount)
                .WithConversion<StringFormatValueConverter>(Strings.RoomsNumFormat).OneWay();
            set.Bind(_amountOfVisitorsButton).For(v => v.Text).To(vm => vm.VisitorsAmount)
                .WithConversion<StringFormatValueConverter>(Strings.PeopleNumFormat).OneWay();

            set.Bind(_cleaningNeededSwitch).For(v => v.Checked).To(vm => vm.CleaningNeeded).TwoWay();
            set.Bind(_nutritionNeededSwitch).For(v => v.Checked).To(vm => vm.NutritionNeeded).TwoWay();
            set.Bind(_breakfastCheckbox).For(v => v.Checked).To(vm => vm.BreakfastIncluded).TwoWay();
            set.Bind(_dinnerCheckbox).For(v => v.Checked).To(vm => vm.DinnerIncluded).TwoWay();
            set.Bind(_supperCheckbox).For(v => v.Checked).To(vm => vm.SupperIncluded).TwoWay();

            set.Bind(_breakfastCheckbox).For(v => v.Enabled).To(vm => vm.NutritionNeeded).OneWay();
            set.Bind(_dinnerCheckbox).For(v => v.Enabled).To(vm => vm.NutritionNeeded).OneWay();
            set.Bind(_supperCheckbox).For(v => v.Enabled).To(vm => vm.NutritionNeeded).OneWay();

            set.Bind(_roomNumberTextField).For(v => v.Activated).To(vm => vm.IsNumberInvalid).OneWay();
            set.Bind(_numberDescriptionTextField).For(v => v.Activated).To(vm => vm.IsDescriptionInvalid).OneWay();

            set.Bind(_selectRoomLevelButton).To(vm => vm.SelectRoomLevelCommand).OneTime();
            set.Bind(_amountOfRoomsButton).To(vm => vm.PromptRoomsAmountCommand).OneTime();
            set.Bind(_amountOfVisitorsButton).To(vm => vm.PromptVisitorsAmountCommand).OneTime();
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
