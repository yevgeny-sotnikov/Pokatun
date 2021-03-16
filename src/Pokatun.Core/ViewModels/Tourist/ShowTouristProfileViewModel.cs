using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Pokatun.Core.Services;
using Pokatun.Core.ViewModels.Profile;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Tourist
{
    public sealed class ShowTouristProfileViewModel : BaseViewModel<TouristDto, object>
    {
        private readonly IPhotosService _photosService;
        private readonly IMvxNavigationService _navigationService;

        public override string Title => FullName;

        public string Placeholder => Title == null ? null : Title[0].ToString();

        private Func<CancellationToken, Task<Stream>> _photoStream;
        public Func<CancellationToken, Task<Stream>> PhotoStream
        {
            get { return _photoStream; }
            set { SetProperty(ref _photoStream, value); }
        }

        private string _fullName = string.Empty;
        public string FullName
        {
            get { return _fullName; }
            set
            {
                SetProperty(ref _fullName, value);
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

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        private TouristDto _parameter;
        private MvxAsyncCommand _editCommand;
        public IMvxAsyncCommand EditCommand
        {
            get
            {
                return _editCommand ?? (_editCommand = new MvxAsyncCommand(DoEditCommandAsync));
            }
        }

        public ShowTouristProfileViewModel(IPhotosService photosService, IMvxNavigationService navigationService)
        {
            _photosService = photosService;
            _navigationService = navigationService;
        }

        public override void Prepare(TouristDto parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (parameter.PhotoName != null)
            {
                PhotoStream = ct => _photosService.GetAsync(parameter.PhotoName);
            }
            else PhotoStream = null;

            FullName = parameter.FullName;
            Email = parameter.Email;
            Phone = parameter.PhoneNumber;

            _parameter = parameter;
        }

        private async Task DoEditCommandAsync()
        {
            TouristDto result = await _navigationService.Navigate<EditTouristProfileViewModel, TouristDto, TouristDto>(_parameter);

            if (result == null)
            {
                return;
            }

            _parameter = result;

            PhotoStream = ct => Task.FromResult<Stream>(File.OpenRead(_parameter.PhotoName));

            FullName = result.FullName;
            Email = result.Email;
            Phone = result.PhoneNumber;

            await Task.WhenAll(RaisePropertyChanged(nameof(Title)), RaisePropertyChanged(nameof(Placeholder)));
        }
    }
}
