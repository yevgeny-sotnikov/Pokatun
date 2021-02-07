using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.Extensions.Caching.Memory;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmValidation;
using Pokatun.Core.Executors;
using Pokatun.Core.Resources;
using Pokatun.Core.Services;
using Pokatun.Data;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core.ViewModels.Tourist
{
    public class EditTouristProfileViewModel : BaseViewModel<TouristDto, TouristDto>
    {
        private readonly INetworkRequestExecutor _networkRequestExecutor;
        private readonly ITouristsService _touristsService;
        private readonly IMvxNavigationService _navigationService;
        private readonly IPhotosService _photosService;
        private readonly IMediaPicker _mediaPicker;
        private readonly IUserDialogs _userDialogs;
        private readonly IMemoryCache _memoryCache;
        private readonly ValidationHelper _validator;

        private bool _viewInEditMode;
        private long _currentTouristId;
        private string _photoFileName;

        public override string Title => Strings.ProfileChanging;

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
                if (!SetProperty(ref _fullName, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsFullnameInvalid));
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

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (!SetProperty(ref _phone, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsPhoneInvalid));
            }
        }

        public bool IsFullnameInvalid => CheckInvalid(nameof(FullName));

        public bool IsEmailInvalid => CheckInvalid(nameof(Email));

        public bool IsPhoneInvalid => CheckInvalid(nameof(Phone));

        private MvxAsyncCommand _closeCommand;
        public IMvxAsyncCommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new MvxAsyncCommand(DoCloseCommandAsync));
            }
        }

        private MvxAsyncCommand _addPhotoCommand;
        public IMvxAsyncCommand AddPhotoCommand
        {
            get
            {
                return _addPhotoCommand ?? (_addPhotoCommand = new MvxAsyncCommand(DoAddPhotoCommandAsync));
            }
        }

        private MvxAsyncCommand _saveChangesCommand;
        public IMvxAsyncCommand SaveChangesCommand
        {
            get
            {
                return _saveChangesCommand ?? (_saveChangesCommand = new MvxAsyncCommand(DoSaveChangesCommandAsync));
            }
        }

        public EditTouristProfileViewModel(
            IMvxNavigationService navigationService,
            IPhotosService photosService,
            IMediaPicker mediaPicker,
            INetworkRequestExecutor networkRequestExecutor,
            ITouristsService touristsService,
            IMemoryCache memoryCache,
            IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _photosService = photosService;
            _mediaPicker = mediaPicker;
            _networkRequestExecutor = networkRequestExecutor;
            _touristsService = touristsService;
            _userDialogs = userDialogs;
            _memoryCache = memoryCache;

            _validator = new ValidationHelper();

            _validator.AddRule(nameof(Email), () => RuleResult.Assert(_viewInEditMode || Regex.IsMatch(Email.Trim(), DataPatterns.Email), Strings.InvalidEmailMessage));
            _validator.AddRule(nameof(Phone), () => RuleResult.Assert(_viewInEditMode || Regex.IsMatch(Phone.Trim(), DataPatterns.Phone), Strings.InvalidPhoneNumberMessage));
            _validator.AddRule(nameof(FullName), () => RuleResult.Assert(_viewInEditMode || !string.IsNullOrWhiteSpace(FullName), Strings.FullnameRequiredMessage));
        }

        public override void Prepare(TouristDto parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            _currentTouristId = parameter.Id;

            _photoFileName = parameter.PhotoName;

            if (parameter.PhotoName != null)
            {
                PhotoStream = ct => _photosService.GetAsync(parameter.PhotoName);
            }
            else PhotoStream = null;

            Email = parameter.Email;
            FullName = parameter.FullName;
            Phone = parameter.PhoneNumber;
        }

        private async Task DoAddPhotoCommandAsync()
        {
            FileResult result = await _mediaPicker.PickPhotoAsync();

            if (result == null)
                return;

            _photoFileName = result.FullPath;
            PhotoStream = ct => result.OpenReadAsync();
        }

        private async Task DoSaveChangesCommandAsync()
        {
            _viewInEditMode = false;

            ValidationResult validationResult = _validator.ValidateAll();

            await Task.WhenAll(
                RaisePropertyChanged(nameof(IsEmailInvalid)),
                RaisePropertyChanged(nameof(IsPhoneInvalid)),
                RaisePropertyChanged(nameof(IsFullnameInvalid))
            );

            if (!validationResult.IsValid)
            {
                _userDialogs.Toast(validationResult.ErrorList[0].ErrorText);

                return;
            }

            ServerResponce<string> responce = await _networkRequestExecutor.MakeRequestAsync(() =>
                _touristsService.SaveChangesAsync(_currentTouristId, FullName, Phone, Email, _photoFileName),
                new HashSet<string>()
            );

            if (responce == null)
                return;

            _memoryCache.Set(
                Constants.Keys.ShortTouristInfo,
                new TouristShortInfoDto { Fullname = FullName, PhotoName = responce.Data }
            );

            await _navigationService.Close(this, new TouristDto
            {
                Id = _currentTouristId,
                FullName = FullName,
                PhoneNumber = Phone,
                Email = Email,
                PhotoName = responce.Data
            });
        }

        private Task DoCloseCommandAsync()
        {
            return _navigationService.Close(this);
        }

        private bool CheckInvalid(string name)
        {
            return !_validator.Validate(name).IsValid;
        }
    }
}
