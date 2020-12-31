using Android.OS;
using Android.Views;
using Android.Widget;
using FFImageLoading.Cross;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.Converters;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.Profile;
using Pokatun.Droid.Controls;
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
        private Button _saveChangesButton;
        private EditText _hotelNameEditText;
        private EditText _fullCompanyNameTextField;
        private MvxLinearLayout _phonesTable;
        private Button _addPhoneButton;
        private EditText _emailTextField;
        private Button _hotelLocationButton;
        private EditText _bankCardOrIbanTextField;
        private EditText _bankNameTextField;
        private EditText _usreouTextField;
        private Button _checkInTimeButton;
        private Button _checkOutTimeButton;
        private MvxLinearLayout _linksTable;
        private Button _addLinkButton;
        private MultilineCountableEditor _withinTerritoryEditText;
        private MultilineCountableEditor _hotelDescriptionEditText;

        protected override int FragmentLayoutId => Resource.Layout.fragment_edit_hotel_profile;

        protected override bool IsHeaderBackButtonVisible => false;
        

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);
            
            _tabHost = view.FindViewById<TabHost>(Android.Resource.Id.TabHost);
            _saveChangesButton = view.FindViewById<Button>(Resource.Id.saveChangesButton);
            _hotelNameEditText = view.FindViewById<EditText>(Resource.Id.hotelNameEditText);
            _fullCompanyNameTextField = view.FindViewById<EditText>(Resource.Id.fullCompanyNameTextField);
            _phonesTable = view.FindViewById<MvxLinearLayout>(Resource.Id.phonesTable);
            _addPhoneButton = view.FindViewById<Button>(Resource.Id.addPhoneButton);
            _emailTextField = view.FindViewById<EditText>(Resource.Id.emailTextField);
            _hotelLocationButton = view.FindViewById<Button>(Resource.Id.hotelLocationButton);
            _bankCardOrIbanTextField = view.FindViewById<EditText>(Resource.Id.bankCardOrIbanTextField);
            _bankNameTextField = view.FindViewById<EditText>(Resource.Id.bankNameTextField);
            _usreouTextField = view.FindViewById<EditText>(Resource.Id.usreouTextField);
            _checkInTimeButton = view.FindViewById<Button>(Resource.Id.checkInTimeButton);
            _checkOutTimeButton = view.FindViewById<Button>(Resource.Id.checkOutTimeButton);
            _linksTable = view.FindViewById<MvxLinearLayout>(Resource.Id.linksTable);
            _addLinkButton = view.FindViewById<Button>(Resource.Id.addLinkButton);
            _withinTerritoryEditText = view.FindViewById<MultilineCountableEditor>(Resource.Id.withinTerritoryEditText);
            _hotelDescriptionEditText = view.FindViewById<MultilineCountableEditor>(Resource.Id.hotelDescriptionEditText);

            _tabHost.Setup();

            _tabHost.AddTab(CreateTab("personalData", Resource.Id.personalDataTab, Resource.Layout.personal_data_tab, Strings.PersonalData));
            _tabHost.AddTab(CreateTab("hotelInfo", Resource.Id.hotelInfoTab, Resource.Layout.hotel_info_tab, Strings.HotelInfo));

            _phonesTable.ItemTemplateId = Resource.Layout.phone_item_template;
            _linksTable.ItemTemplateId = Resource.Layout.link_item_template;

            _saveChangesButton.Text = Strings.SaveChanges;
            _hotelNameEditText.Hint = Strings.Name;
            _fullCompanyNameTextField.Hint = Strings.FullCompanyName;
            _addPhoneButton.Text = Strings.AddPhone;
            _emailTextField.Hint = Strings.Email;
            _hotelLocationButton.Text = Strings.HotelLocationAddress;
            _bankCardOrIbanTextField.Hint = Strings.CardNumberOrIBAN;
            _bankNameTextField.Hint = Strings.BankName;
            _usreouTextField.Hint = Strings.USREOU;
            _addLinkButton.Text = Strings.AddLink;

            _withinTerritoryEditText.Title = Strings.WithinTerritory;
            _withinTerritoryEditText.Hint = Strings.WithinTerritoryFillingInstruction;
            _hotelDescriptionEditText.Title = Strings.Description;
            _hotelDescriptionEditText.Hint = Strings.DescriptionFillingInstruction;

            _withinTerritoryEditText.MaxLenght = 200;
            _hotelDescriptionEditText.MaxLenght = 600;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(ToolbarRightButton).For(ToolbarRightButton.BindClick()).To(vm => vm.CloseCommand).OneTime();
            set.Bind(ToolbarAddPhotoButton).For(ToolbarAddPhotoButton.BindClick()).To(vm => vm.AddPhotoCommand).OneTime();
            set.Bind(ToolbarPhotoView).For(v => v.ImagePath).To(vm => vm.PhotoFilePath).OneWay();

            set.Bind(_hotelNameEditText).To(vm => vm.HotelName).TwoWay();
            set.Bind(_fullCompanyNameTextField).To(vm => vm.FullCompanyName).TwoWay();
            set.Bind(_emailTextField).To(vm => vm.Email).TwoWay();
            set.Bind(_bankCardOrIbanTextField).To(vm => vm.BankCardOrIban).TwoWay();
            set.Bind(_bankNameTextField).To(vm => vm.BankName).TwoWay();
            set.Bind(_usreouTextField).To(vm => vm.USREOU).TwoWay();
            set.Bind(_withinTerritoryEditText).For(v => v.Data).To(vm => vm.WithinTerritoryDescription).TwoWay();
            set.Bind(_hotelDescriptionEditText).For(v => v.Data).To(vm => vm.HotelDescription).TwoWay();

            set.Bind(_checkInTimeButton).For(v => v.Text).To(vm => vm.CheckInTime).WithConversion<TimeConverter>(Strings.CheckInTime).OneWay();
            set.Bind(_checkOutTimeButton).For(v => v.Text).To(vm => vm.CheckOutTime).WithConversion<TimeConverter>(Strings.CheckOutTime).OneWay();

            set.Bind(_phonesTable).For(v => v.ItemsSource).To(vm => vm.PhoneNumbers).OneTime();
            set.Bind(_linksTable).For(v => v.ItemsSource).To(vm => vm.SocialResources).OneTime();

            set.Bind(_hotelNameEditText).For(v => v.Activated).To(vm => vm.IsHotelNameInvalid).OneWay();
            set.Bind(_fullCompanyNameTextField).For(v => v.Activated).To(vm => vm.IsFullCompanyNameInvalid).OneWay();
            set.Bind(_usreouTextField).For(v => v.Activated).To(vm => vm.IsUsreouInvalid).OneWay();
            set.Bind(_bankCardOrIbanTextField).For(v => v.Activated).To(vm => vm.IsBankCardOrIbanInvalid).OneWay();
            set.Bind(_bankNameTextField).For(v => v.Activated).To(vm => vm.IsBankNameInvalid).OneWay();
            set.Bind(_emailTextField).For(v => v.Activated).To(vm => vm.IsEmailInvalid).OneWay();
            set.Bind(_checkInTimeButton).For(v => v.Activated).To(vm => vm.IsCheckInTimeInvalid).OneWay();
            set.Bind(_checkOutTimeButton).For(v => v.Activated).To(vm => vm.IsCheckOutTimeInvalid).OneWay();
            set.Bind(_withinTerritoryEditText).For(v => v.Activated).To(vm => vm.IsWithinTerritoryDescriptionInvalid).OneWay();
            set.Bind(_hotelDescriptionEditText).For(v => v.Activated).To(vm => vm.IsHotelDescriptionInvalid).OneWay();

            set.Bind(_addPhoneButton).To(vm => vm.AddPhoneCommand).OneTime();
            set.Bind(_addLinkButton).To(vm => vm.AddSocialResourceCommand).OneTime();
            set.Bind(_checkInTimeButton).To(vm => vm.ChooseCheckInTimeCommand).OneTime();
            set.Bind(_checkOutTimeButton).To(vm => vm.ChooseCheckOutTimeCommand).OneTime();
            set.Bind(_saveChangesButton).To(vm => vm.SaveChangesCommand).OneTime();

            set.Apply();

            return view;
        }

        private TabSpec CreateTab(string tag, int contentId, int tabLayoutId, string title)
        {
            TabSpec tabSpec = _tabHost.NewTabSpec(tag);
            tabSpec.SetContent(contentId);

            View view = LayoutInflater.Inflate(tabLayoutId, null);

            TextView tabLabel = view.FindViewById<TextView>(Resource.Id.tabLabel);
            tabLabel.Text = title;

            tabSpec.SetIndicator(view);

            return tabSpec;
        }

        public override void OnStart()
        {
            base.OnStart();

            ToolbarRightButton.SetImageResource(Resource.Drawable.close);
            ToolbarRightButton.Visibility = ViewStates.Visible;
            ToolbarAddPhotoButton.Visibility = ViewStates.Visible;
            ToolbarPhotoView.Visibility = ViewStates.Visible;
        }
    }
}
