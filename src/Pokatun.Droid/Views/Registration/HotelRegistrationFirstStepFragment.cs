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
    public sealed class HotelRegistrationFirstStepFragment : BaseFragment<HotelRegistrationFirstStepViewModel>
    {
        private EditText _hotelNameEditText;
        private EditText _phoneNumberEditText;
        private EditText _emailEditText;
        private EditText _passwordEditText;
        private EditText _confirmPasswordEditText;
        private Button _furtherButton;

        protected override int FragmentLayoutId => Resource.Layout.fragment_hotel_registration_first_step;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _hotelNameEditText = view.FindViewById<EditText>(Resource.Id.hotelNameEditText);
            _phoneNumberEditText = view.FindViewById<EditText>(Resource.Id.phoneNumberEditText);
            _emailEditText = view.FindViewById<EditText>(Resource.Id.emailEditText);
            _passwordEditText = view.FindViewById<EditText>(Resource.Id.passwordEditText);
            _confirmPasswordEditText = view.FindViewById<EditText>(Resource.Id.confirmPasswordEditText);
            _furtherButton = view.FindViewById<Button>(Resource.Id.furtherButton);

            _hotelNameEditText.Hint = Strings.Hotel;
            _phoneNumberEditText.Hint = Strings.Phone;
            _passwordEditText.Hint = Strings.Password;
            _confirmPasswordEditText.Hint = Strings.ConfirmPassword;
            _furtherButton.Text = Strings.Further;

            return view;
        }
    }
}
