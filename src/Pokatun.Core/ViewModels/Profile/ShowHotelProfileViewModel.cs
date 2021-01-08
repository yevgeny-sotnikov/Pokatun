using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Pokatun.Core.Enums;
using Pokatun.Core.Services;
using Pokatun.Core.ViewModels.Collections;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Profile
{
    public class ShowHotelProfileViewModel : BaseViewModel<HotelDto>
    {
        private readonly IPhotosService _photosService;
        private readonly IMvxNavigationService _navigationService;

        private HotelDto _parameter;

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

        private TimeSpan? _checkInTime;
        public TimeSpan? CheckInTime
        {
            get { return _checkInTime; }
            set
            {
                SetProperty(ref _checkInTime, value);
            }
        }

        private TimeSpan? _checkOutTime;
        public TimeSpan? CheckOutTime
        {
            get { return _checkOutTime; }
            set
            {
                SetProperty(ref _checkOutTime, value);
            }
        }


        public MvxObservableCollection<EntryItemViewModel> PhoneNumbers { get; private set; }

        public MvxObservableCollection<EntryItemViewModel> SocialResources { get; private set; }

        private MvxAsyncCommand _editCommand;
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

            PhoneNumbers = new MvxObservableCollection<EntryItemViewModel>();
            SocialResources = new MvxObservableCollection<EntryItemViewModel>();
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
            CheckInTime = parameter.CheckInTime;
            CheckOutTime = parameter.CheckOutTime;

            PhoneNumbers.AddRange(parameter.Phones.Select(p => new EntryItemViewModel { Type = EntryType.Phone, Text = p.Number }));
            SocialResources.AddRange(parameter.SocialResources.Select(sr => new EntryItemViewModel { Type = EntryType.Link, Text = sr.Link }));

            _parameter = parameter;
        }

        private async Task DoEditCommandAsync()
        {
            HotelDto result = await _navigationService.Navigate<EditHotelProfileViewModel, HotelDto, HotelDto>(_parameter);

            if (result == null)
            {
                return;
            }

            PhotoStream = ct => Task.FromResult<Stream>(File.OpenRead(result.PhotoUrl));

            _parameter.HotelName = result.HotelName;
            _parameter.FullCompanyName = result.FullCompanyName;
            _parameter.Email = result.Email;
            _parameter.BankCard = result.BankCard;
            _parameter.IBAN = result.IBAN;
            _parameter.BankName = result.BankName;
            _parameter.USREOU = result.USREOU;
            _parameter.HotelDescription = result.HotelDescription;
            _parameter.WithinTerritoryDescription = result.WithinTerritoryDescription;
            _parameter.CheckInTime = result.CheckInTime;
            _parameter.CheckOutTime = result.CheckOutTime;
            _parameter.Phones = result.Phones;
            _parameter.SocialResources = result.SocialResources;

            HotelName = result.HotelName;
            FullCompanyName = result.FullCompanyName;
            Email = result.Email;
            BankCardOrIban = result.IBAN ?? result.BankCard.ToString();
            BankName = result.BankName;
            USREOU = result.USREOU.ToString();
            HotelDescription = result.HotelDescription;
            WithinTerritoryDescription = result.WithinTerritoryDescription;
            CheckInTime = result.CheckInTime;
            CheckOutTime = result.CheckOutTime;

            PhoneNumbers.Clear();
            SocialResources.Clear();

            PhoneNumbers.AddRange(result.Phones.Select(p => new EntryItemViewModel { Type = EntryType.Phone, Text = p.Number }));
            SocialResources.AddRange(result.SocialResources.Select(sr => new EntryItemViewModel { Type = EntryType.Link, Text = sr.Link }));
        }
    }
}
