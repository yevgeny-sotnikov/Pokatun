using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using Pokatun.Core.Models.Enums;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.ChoiseUserRole;
using Pokatun.Core.ViewModels.Main;

namespace Pokatun.Droid.Views.ChoiseUserRole
{
    [MvxFragmentPresentation(typeof(MainContainerViewModel), Resource.Id.content_frame, AddToBackStack = true)]
    public sealed class ChoiseUserRoleFragment : BaseFragment<ChoiseUserRoleViewModel>
    {
        private TextView _chooseRoleLabel;
        private Button _touristButton;
        private TextView _touristDescriptionLabel;
        private Button _hotelButton;
        private TextView _hotelDescriptionLabel;

        protected override int FragmentLayoutId => Resource.Layout.fragment_choise_user_role;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _chooseRoleLabel = view.FindViewById<TextView>(Resource.Id.chooseRoleLabel);
            _touristButton = view.FindViewById<Button>(Resource.Id.touristButton);
            _touristDescriptionLabel = view.FindViewById<TextView>(Resource.Id.touristDescriptionLabel);
            _hotelButton = view.FindViewById<Button>(Resource.Id.hotelButton);
            _hotelDescriptionLabel = view.FindViewById<TextView>(Resource.Id.hotelDescriptionLabel);

            _chooseRoleLabel.Text = Strings.ChooseRoleText;
            _touristButton.Text = Strings.Tourist;
            _touristDescriptionLabel.Text = Strings.TouristRoleDescriptionText;
            _hotelButton.Text = Strings.Hotel;
            _hotelDescriptionLabel.Text = Strings.HotelRoleDescriptionText;

            IMvxFragmentView<ChoiseUserRoleViewModel> v = this;
            MvxFluentBindingDescriptionSet<IMvxFragmentView<ChoiseUserRoleViewModel>, ChoiseUserRoleViewModel> set = CreateBindingSet();

            set.Bind(_touristButton).For(nameof(_touristButton.Click)).To(vm => vm.RoleChoosedCommand).CommandParameter(UserRole.Tourist);
            set.Bind(_hotelButton).For(nameof(_hotelButton.Click)).To(vm => vm.RoleChoosedCommand).CommandParameter(UserRole.HotelAdministrator);

            set.Apply();

            return view;
        }
    }
}
