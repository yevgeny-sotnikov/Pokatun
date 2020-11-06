using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.Registration;
using Pokatun.Droid.Views.PreEntrance;

namespace Pokatun.Droid.Views.Registration
{
    public sealed class HotelRegistrationSecondStepFragment : BaseFragment<HotelRegistrationSecondStepViewModel>, IMvxOverridePresentationAttribute
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
            _createAccountButton.Text = Strings.CreateAccount;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

#pragma warning restore IDE0008 // Use explicit type

            set.Bind(_fullCompanyNameTextField).To(vm => vm.FullCompanyName).OneWayToSource();
            set.Bind(_bankCardOrIbanTextField).To(vm => vm.BankCardOrIban).OneWayToSource();
            set.Bind(_bankNameTextField).To(vm => vm.BankName).OneWayToSource();
            set.Bind(_usreouTextField).To(vm => vm.USREOU).OneWayToSource();
            set.Bind(_createAccountButton).To(vm => vm.СreateAccountCommand);
            _createAccountButton.Click += dd;
            set.Bind(_fullCompanyNameTextField).For(v => v.Activated).To(vm => vm.IsFullCompanyNameInvalid).OneWay();
            set.Bind(_bankCardOrIbanTextField).For(v => v.Activated).To(vm => vm.IsBankCardOrIbanInvalid).OneWay();
            set.Bind(_bankNameTextField).For(v => v.Activated).To(vm => vm.IsBankNameInvalid).OneWay();
            set.Bind(_usreouTextField).For(v => v.Activated).To(vm => vm.IsUsreouInvalid).OneWay();

            set.Apply();

            return view;
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            return new MvxFragmentPresentationAttribute
            {
                ActivityHostViewModelType = typeof(MainContainerViewModel),
                FragmentContentId = Resource.Id.content_frame,
                AddToBackStack = true,
                EnterAnimation = Android.Resource.Animation.FadeIn,
                PopEnterAnimation = Android.Resource.Animation.FadeIn,
                ExitAnimation = Android.Resource.Animation.SlideOutRight,
                PopExitAnimation = Android.Resource.Animation.SlideOutRight,
                PopBackStackImmediateName = typeof(PreEntranceFragment).FragmentJavaName(),
                PopBackStackImmediateFlag = MvxPopBackStack.Inclusive
            };
        }

        private void dd(object sender, EventArgs e)
        {
            //var fm = Activity.SupportFragmentManager;
            //var f = fm.GetBackStackEntryAt(0);
            //string n = typeof(PreEntranceFragment).FragmentJavaName();
            //fm.PopBackStack(f.Name, (int)MvxPopBackStack.Inclusive.ToNativePopBackStackFlags());
        }
    }
}
