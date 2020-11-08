
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
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

            return view;
        }
    }
}
