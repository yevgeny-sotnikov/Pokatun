using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.Registration;

namespace Pokatun.Droid.Views.Registration
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
    public sealed class HotelRegistrationSecondStepFragment : BaseFragment<HotelRegistrationSecondStepViewModel>
    {
        private EditText _fullCompanyNameTextField;
        private EditText _bankCardOrIbanTextField;
        private EditText _bankNameTextField;
        private EditText _usreouTextField;
        private Button _createAccountButton;

        protected override int FragmentLayoutId => Resource.Layout.fragment_hotel_registration_second_step;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _fullCompanyNameTextField = view.FindViewById<EditText>(Resource.Id.fullCompanyNameTextField);
            _bankCardOrIbanTextField = view.FindViewById<EditText>(Resource.Id.bankCardOrIbanTextField);
            _bankNameTextField = view.FindViewById<EditText>(Resource.Id.bankNameTextField);
            _usreouTextField = view.FindViewById<EditText>(Resource.Id.usreouTextField);
            _createAccountButton = view.FindViewById<Button>(Resource.Id.createAccountButton);

            _fullCompanyNameTextField.Hint = Strings.FullCompanyName;
            _bankCardOrIbanTextField.Hint = Strings.CardNumberOrIBAN;
            _bankNameTextField.Hint = Strings.BankName;
            _usreouTextField.Hint = Strings.USREOU;
            _createAccountButton.Text = Strings.Further;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            //set.Bind(_hotelNameEditText).To(vm => vm.HotelName).OneWayToSource();
            //set.Bind(_phoneNumberEditText).To(vm => vm.PhoneNumber).OneWayToSource();
            //set.Bind(_emailEditText).To(vm => vm.Email).OneWayToSource();
            //set.Bind(_passwordEditText).To(vm => vm.Password).OneWayToSource();
            //set.Bind(_confirmPasswordEditText).To(vm => vm.PasswordConfirm).OneWayToSource();
            //set.Bind(_furtherButton).To(vm => vm.FurtherCommand);

            //set.Bind(_hotelNameEditText).For(v => v.Activated).To(vm => vm.IsHotelNameInvalid).OneWay();
            //set.Bind(_phoneNumberEditText).For(v => v.Activated).To(vm => vm.IsPhoneNumberInvalid).OneWay();
            //set.Bind(_emailEditText).For(v => v.Activated).To(vm => vm.IsEmailInvalid).OneWay();
            //set.Bind(_passwordEditText).For(v => v.Activated).To(vm => vm.IsPasswordInvalid).OneWay();
            //set.Bind(_confirmPasswordEditText).For(v => v.Activated).To(vm => vm.IsPasswordConfirmInvalid).OneWay();

            set.Apply();

            return view;
        }
    }
}
