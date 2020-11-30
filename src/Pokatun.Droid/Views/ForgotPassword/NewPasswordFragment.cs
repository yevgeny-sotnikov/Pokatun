
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
using MvvmCross.Platforms.Android.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.ForgotPassword;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Droid.Views.PreEntrance;

namespace Pokatun.Droid.Views.ForgotPassword
{
    public sealed class NewPasswordFragment : BaseFragment<NewPasswordViewModel>, IMvxOverridePresentationAttribute
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

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(_passwordEditText).To(vm => vm.Password).OneWayToSource();
            set.Bind(_confirmPasswordEditText).To(vm => vm.PasswordConfirm).OneWayToSource();
            set.Bind(_saveButton).To(vm => vm.SavePasswordCommand);

            set.Bind(_passwordEditText).For(v => v.Activated).To(vm => vm.IsPasswordInvalid).OneWay();
            set.Bind(_confirmPasswordEditText).For(v => v.Activated).To(vm => vm.IsPasswordConfirmInvalid).OneWay();

            set.Apply();

            return view;
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            return Utils.CreatePopBackStackAttribute();
        }
    }
}
