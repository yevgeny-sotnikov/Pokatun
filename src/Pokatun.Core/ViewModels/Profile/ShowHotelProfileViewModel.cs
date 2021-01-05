using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Pokatun.Core.Services;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Profile
{
    public class ShowHotelProfileViewModel : BaseViewModel<HotelDto>
    {
        private readonly IPhotosService _photosService;
        private readonly IMvxNavigationService _navigationService;

        private Func<CancellationToken, Task<Stream>> _photoStream;
        public Func<CancellationToken, Task<Stream>> PhotoStream
        {
            get { return _photoStream; }
            set { SetProperty(ref _photoStream, value); }
        }

        private string _hotelName = string.Empty;
        public string HotelName
        {
            get { return _hotelName; }
            set
            {
                SetProperty(ref _hotelName, value);
            }
        }

        private string _fullCompanyName = string.Empty;
        public string FullCompanyName
        {
            get { return _fullCompanyName; }
            set
            {
                SetProperty(ref _fullCompanyName, value);
            }
        }

        private string _email = string.Empty;
        public string Email
        {
            get { return _email; }
            set
            {
                SetProperty(ref _email, value);
            }
        }

        private string _bankCardOrIban = string.Empty;
        public string BankCardOrIban
        {
            get { return _bankCardOrIban; }
            set
            {
                SetProperty(ref _bankCardOrIban, value);
            }
        }

        private string _bankName = string.Empty;
        public string BankName
        {
            get { return _bankName; }
            set
            {
                SetProperty(ref _bankName, value);
            }
        }

        private string _usreou = string.Empty;
        public string USREOU
        {
            get { return _usreou; }
            set
            {
                SetProperty(ref _usreou, value);
            }
        }

        private string _withinTerritoryDescription;
        public string WithinTerritoryDescription
        {
            get { return _withinTerritoryDescription; }
            set
            {
                SetProperty(ref _withinTerritoryDescription, value);
            }
        }

        private string _hotelDescription;
        public string HotelDescription
        {
            get { return _hotelDescription; }
            set
            {
                SetProperty(ref _hotelDescription, value);
            }
        }

        private MvxAsyncCommand _editCommand;
        private HotelDto _parameter;

        public IMvxAsyncCommand EditCommand
        {
            get
            {
                return _editCommand ?? (_editCommand = new MvxAsyncCommand(DoEditCommandAsync));
            }
        }

        public ShowHotelProfileViewModel(IPhotosService photosService, IMvxNavigationService navigationService)
        {
            _photosService = photosService;
            _navigationService = navigationService;
        }

        public override void Prepare(HotelDto parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (parameter.PhotoUrl != null)
            {
                PhotoStream = ct => _photosService.GetAsync(parameter.PhotoUrl);
            }
            else PhotoStream = null;

            HotelName = parameter.HotelName;
            FullCompanyName = parameter.FullCompanyName;
            Email = parameter.Email;
            BankCardOrIban = parameter.BankCard == null ? parameter.IBAN : parameter.BankCard.ToString();
            BankName = parameter.BankName;
            USREOU = parameter.USREOU.ToString();
            HotelDescription = parameter.HotelDescription;
            WithinTerritoryDescription = parameter.WithinTerritoryDescription;

            _parameter = parameter;
        }

        private Task DoEditCommandAsync()
        {
            return _navigationService.Navigate<EditHotelProfileViewModel, HotelDto>(_parameter);
        }
    }
}
