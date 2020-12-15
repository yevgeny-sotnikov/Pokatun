using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmValidation;
using Pokatun.Core.Resources;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Profile
{
    public sealed class EditHotelProfileViewModel : BaseViewModel<HotelDto, bool>
    {
        private readonly IMvxNavigationService _navigationService;

        private bool _viewInEditMode = true;
        private readonly ValidationHelper _validator;

        private string _hotelName = string.Empty;
        public string HotelName
        {
            get { return _hotelName; }
            set
            {
                if (!SetProperty(ref _hotelName, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsHotelNameInvalid));
            }
        }

        private string _fullCompanyName = string.Empty;
        public string FullCompanyName
        {
            get { return _fullCompanyName; }
            set
            {
                if (!SetProperty(ref _fullCompanyName, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsFullCompanyNameInvalid));
            }
        }

        public bool IsHotelNameInvalid => CheckInvalid(nameof(HotelName));

        public bool IsFullCompanyNameInvalid => CheckInvalid(nameof(FullCompanyName));

        private MvxAsyncCommand _closeCommand;
        public IMvxAsyncCommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new MvxAsyncCommand(DoCloseCommandAsync));
            }
        }

        public EditHotelProfileViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            _validator = new ValidationHelper();

            _validator.AddRule(nameof(HotelName), () => RuleResult.Assert(_viewInEditMode || !string.IsNullOrWhiteSpace(HotelName), Strings.HotelNameRequiredMessage));
            _validator.AddRule(nameof(FullCompanyName), () => RuleResult.Assert(_viewInEditMode || !string.IsNullOrWhiteSpace(FullCompanyName), Strings.CompanyNameRequiredMessage));
        }

        public override void Prepare(HotelDto parameter)
        {
            HotelName = parameter.HotelName;
            FullCompanyName = parameter.FullCompanyName;
        }

        private Task DoCloseCommandAsync()
        {
            return _navigationService.Close(this, false);
        }

        private bool CheckInvalid(string name)
        {
            return !_validator.Validate(name).IsValid;
        }
    }
}
