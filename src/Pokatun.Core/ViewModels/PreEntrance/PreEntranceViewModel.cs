using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Pokatun.Core.Models.Enums;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Registration;

namespace Pokatun.Core.ViewModels.PreEntrance
{
    public sealed class PreEntranceViewModel : BaseViewModel<UserRole>
    {
        private readonly IMvxNavigationService _navigationService;

        private UserRole _role;

        public string PreEntranceDescriptionText => _role == UserRole.Tourist
            ? Strings.PreEntranceTouristDescriptionText
            : Strings.PreEntranceHotelDescriptionText;

        private IMvxCommand _registrationCommand;
        public IMvxCommand RegistrationCommand
        {
            get { return _registrationCommand ?? (_registrationCommand = new MvxAsyncCommand(OnRegistrationCommandAsync)); }
        }

        public PreEntranceViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Prepare(UserRole role)
        {
            _role = role;
        }

        private Task OnRegistrationCommandAsync()
        {
            return _navigationService.Navigate<HotelRegistrationFirstStepViewModel>();
        }
    }
}
