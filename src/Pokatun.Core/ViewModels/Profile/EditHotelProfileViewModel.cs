using System;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
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

        private string _email = string.Empty;
        public string Email
        {
            get { return _email; }
            set
            {
                if (!SetProperty(ref _email, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsEmailInvalid));
            }
        }

        private string _bankCardOrIban = string.Empty;
        public string BankCardOrIban
        {
            get { return _bankCardOrIban; }
            set
            {
                if (!SetProperty(ref _bankCardOrIban, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsBankCardOrIbanInvalid));
            }
        }

        private string _bankName = string.Empty;
        public string BankName
        {
            get { return _bankName; }
            set
            {
                if (!SetProperty(ref _bankName, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsBankNameInvalid));
            }
        }

        private string _usreou = string.Empty;
        public string USREOU
        {
            get { return _usreou; }
            set
            {
                if (!SetProperty(ref _usreou, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsUsreouInvalid));
            }
        }

        public MvxObservableCollection<PhoneItemViewModel> PhoneNumbers { get; private set; }

        public bool IsHotelNameInvalid => CheckInvalid(nameof(HotelName));

        public bool IsFullCompanyNameInvalid => CheckInvalid(nameof(FullCompanyName));

        public bool IsEmailInvalid => CheckInvalid(nameof(Email));

        public bool IsUsreouInvalid => CheckInvalid(nameof(USREOU));

        public bool IsBankNameInvalid => CheckInvalid(nameof(BankName));

        public bool IsBankCardOrIbanInvalid => CheckInvalid(nameof(BankCardOrIban));

        private MvxAsyncCommand _closeCommand;
        public IMvxAsyncCommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new MvxAsyncCommand(DoCloseCommandAsync));
            }
        }

        private MvxCommand _addPhoneCommand;
        public IMvxCommand AddPhoneCommand
        {
            get
            {
                return _addPhoneCommand ?? (_addPhoneCommand = new MvxCommand(DoAddPhoneCommand));
            }
        }

        private MvxCommand<PhoneItemViewModel> _deletePhoneCommand;
        public IMvxCommand<PhoneItemViewModel> DeletePhoneCommand
        {
            get
            {
                return _deletePhoneCommand ?? (_deletePhoneCommand = new MvxCommand<PhoneItemViewModel>(DoDeletePhoneCommand));
            }
        }

        public EditHotelProfileViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            _validator = new ValidationHelper();

            _validator.AddRule(nameof(HotelName), () => RuleResult.Assert(_viewInEditMode || !string.IsNullOrWhiteSpace(HotelName), Strings.HotelNameRequiredMessage));
            _validator.AddRule(nameof(FullCompanyName), () => RuleResult.Assert(_viewInEditMode || !string.IsNullOrWhiteSpace(FullCompanyName), Strings.CompanyNameRequiredMessage));

            PhoneNumbers = new MvxObservableCollection<PhoneItemViewModel>();
        }

        public override void Prepare(HotelDto parameter)
        {
            HotelName = parameter.HotelName;
            FullCompanyName = parameter.FullCompanyName;
            Email = parameter.Email;
            BankCardOrIban = parameter.BankCard == null ? parameter.IBAN : parameter.BankCard.ToString();
            BankName = parameter.BankName;
            USREOU = parameter.USREOU.ToString();

            PhoneNumbers.AddRange(parameter.Phones.Select(p => new PhoneItemViewModel(p, DeletePhoneCommand)));
        }

        private void DoAddPhoneCommand()
        {
            PhoneNumbers.Add(new PhoneItemViewModel(null, DeletePhoneCommand));
        }

        private void DoDeletePhoneCommand(PhoneItemViewModel phoneVM)
        {
            PhoneNumbers.Remove(phoneVM);
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
