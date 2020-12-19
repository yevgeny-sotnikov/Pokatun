using System;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Profile
{
    public sealed class PhoneItemViewModel : MvxViewModel
    {
        private readonly long? _storedId;
        private readonly IMvxCommand<PhoneItemViewModel> _deletePhoneCommand;
        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
        }

        private MvxCommand _deleteItemCommand;
        public IMvxCommand DeleteItemCommand
        {
            get
            {
                return _deleteItemCommand ?? (_deleteItemCommand = new MvxCommand(
                    () => _deletePhoneCommand.Execute(this),
                    () => _deletePhoneCommand.CanExecute(this)
                ));
            }
        }

        public PhoneDto CreatePhoneDto()
        {
            return new PhoneDto { Id = _storedId == null ? 0 : (long)_storedId, Number = PhoneNumber };
        }

        public PhoneItemViewModel(PhoneDto phone, IMvxCommand<PhoneItemViewModel> deletePhoneCommand)
        {
            if (deletePhoneCommand == null)
            {
                throw new ArgumentNullException(nameof(deletePhoneCommand));
            }

            _storedId = phone == null ? 0 : phone.Id;
            PhoneNumber = phone == null ? string.Empty : phone.Number;

            _deletePhoneCommand = deletePhoneCommand;
        }
    }
}
