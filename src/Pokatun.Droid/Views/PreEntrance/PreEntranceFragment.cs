using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.PreEntrance;

namespace Pokatun.Droid.Views.PreEntrance
{
    [MvxFragmentPresentation(typeof(MainContainerViewModel), Resource.Id.content_frame, AddToBackStack = true)]
    public sealed class PreEntranceFragment : BaseFragment<PreEntranceViewModel>
    {
        private TextView _helloLabel;
        private TextView _preEntranceDescriptionLabel;
        private Button _registrationButton;
        private TextView _iStillDoesntHaveAccLabel;
        private Button _loginButton;
        private TextView _iHaveAccLabel;

        protected override int FragmentLayoutId => Resource.Layout.fragment_pre_entrance;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _helloLabel = view.FindViewById<TextView>(Resource.Id.helloLabel);
            _preEntranceDescriptionLabel = view.FindViewById<TextView>(Resource.Id.preEntranceDescriptionLabel);
            _registrationButton = view.FindViewById<Button>(Resource.Id.registrationButton);
            _iStillDoesntHaveAccLabel = view.FindViewById<TextView>(Resource.Id.iStillDoesntHaveAccLabel);
            _loginButton = view.FindViewById<Button>(Resource.Id.loginButton);
            _iHaveAccLabel = view.FindViewById<TextView>(Resource.Id.iHaveAccLabel);

            _helloLabel.Text = Strings.HelloCaps;
            _registrationButton.Text = Strings.Registration;
            _iStillDoesntHaveAccLabel.Text = Strings.IStillDoesntHaveAccText;
            _loginButton.Text = Strings.Entrance;
            _iHaveAccLabel.Text = Strings.IHaveAccText;

            MvxFluentBindingDescriptionSet<IMvxFragmentView<PreEntranceViewModel>, PreEntranceViewModel> set = CreateBindingSet();

            set.Bind(_preEntranceDescriptionLabel).To(vm => vm.PreEntranceDescriptionText);

            set.Apply();


            return view;
        }

    }
}
