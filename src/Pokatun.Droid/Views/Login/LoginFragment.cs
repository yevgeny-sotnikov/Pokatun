using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Login;
using Pokatun.Core.ViewModels.Main;

namespace Pokatun.Droid.Views.Login
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
    public class LoginFragment : BaseFragment<LoginViewModel>
    {
        private EditText _emailTextField;
        private EditText _passwordTextField;
        private Button _forgotPasswordButton;
        private Button _loginButton;

        protected override int FragmentLayoutId => Resource.Layout.fragment_login;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment

            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _emailTextField = view.FindViewById<EditText>(Resource.Id.emailTextField);
            _passwordTextField = view.FindViewById<EditText>(Resource.Id.passwordTextField);
            _forgotPasswordButton = view.FindViewById<Button>(Resource.Id.forgotPasswordButton);
            _loginButton = view.FindViewById<Button>(Resource.Id.loginButton);

            _emailTextField.Hint = Strings.Email;
            _passwordTextField.Hint = Strings.Password;
            _forgotPasswordButton.Text = Strings.ForgotPasswordQuestion;
            _loginButton.Text = Strings.ToComeIn;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(_emailTextField).To(vm => vm.Email).OneWayToSource();
            set.Bind(_passwordTextField).To(vm => vm.Password).OneWayToSource();
            set.Bind(_loginButton).To(vm => vm.LoginCommand);
            set.Bind(_forgotPasswordButton).To(vm => vm.ForgotPasswordCommand);

            set.Bind(_emailTextField).For(v => v.Activated).To(vm => vm.IsEmailInvalid).OneWay();
            set.Bind(_passwordTextField).For(v => v.Activated).To(vm => vm.IsPasswordInvalid).OneWay();

            set.Apply();

            return view;
        }
    }
}
