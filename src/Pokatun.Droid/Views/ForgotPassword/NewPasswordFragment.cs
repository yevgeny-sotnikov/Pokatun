
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
using Pokatun.Core.ViewModels.ForgotPassword;
using Pokatun.Core.ViewModels.Main;

namespace Pokatun.Droid.Views.ForgotPassword
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
    public sealed class NewPasswordFragment : BaseFragment<NewPasswordViewModel>
    {
        private EditText _passwordEditText;
        private EditText _confirmPasswordEditText;
        private Button _saveButton;

        protected override int FragmentLayoutId => Resource.Layout.fragment_new_password;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _passwordEditText = view.FindViewById<EditText>(Resource.Id.passwordEditText);
            _confirmPasswordEditText = view.FindViewById<EditText>(Resource.Id.confirmPasswordEditText);
            _saveButton = view.FindViewById<Button>(Resource.Id.saveButton);

            _passwordEditText.Hint = Strings.Password;
            _confirmPasswordEditText.Hint = Strings.ConfirmPassword;
            _saveButton.Text = Strings.SavePassword;

            return view;
        }
    }
}
