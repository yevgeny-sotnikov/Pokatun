using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.Resources;
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
        private Button _saveChangesButton;
        private EditText _hotelNameEditText;
        private EditText _fullCompanyNameTextField;

        protected override int FragmentLayoutId => Resource.Layout.fragment_edit_hotel_profile;

        protected override bool IsHeaderBackButtonVisible => false;
        

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _tabHost = view.FindViewById<TabHost>(Android.Resource.Id.TabHost);
            _saveChangesButton = view.FindViewById<Button>(Resource.Id.saveChangesButton);
            _hotelNameEditText = view.FindViewById<EditText>(Resource.Id.hotelNameEditText);
            _fullCompanyNameTextField = view.FindViewById<EditText>(Resource.Id.fullCompanyNameTextField);

            _tabHost.Setup();

            _tabHost.AddTab(CreateTab("personalData", Resource.Id.personalDataTab, Resource.Layout.personal_data_tab, Strings.PersonalData));
            _tabHost.AddTab(CreateTab("hotelInfo", Resource.Id.hotelInfoTab, Resource.Layout.hotel_info_tab, Strings.HotelInfo));

            _saveChangesButton.Text = Strings.SaveChanges;
            _hotelNameEditText.Hint = Strings.Name;
            _fullCompanyNameTextField.Hint = Strings.FullCompanyName;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(ToolbarRightButton).For(ToolbarRightButton.BindClick()).To(vm => vm.CloseCommand);
            set.Bind(_hotelNameEditText).For(v => v.Text).To(vm => vm.HotelName).TwoWay();
            set.Bind(_fullCompanyNameTextField).For(v => v.Text).To(vm => vm.FullCompanyName).TwoWay();

            set.Bind(_hotelNameEditText).For(v => v.Activated).To(vm => vm.IsHotelNameInvalid).OneWay();
            set.Bind(_fullCompanyNameTextField).For(v => v.Activated).To(vm => vm.IsFullCompanyNameInvalid).OneWay();

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

        }
    }
}
