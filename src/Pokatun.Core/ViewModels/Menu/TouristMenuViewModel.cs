using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Pokatun.Core.ViewModels.ChoiseUserRole;
using Pokatun.Data;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core.ViewModels.Menu
{
    public sealed class TouristMenuViewModel: BaseViewModel<TouristShortInfoDto>
    {

        private string _title;
        public override string Title => _title;

        public string Placeholder => Title == null ? null : Title[0].ToString();

        private Func<CancellationToken, Task<Stream>> _photoStream;
        public Func<CancellationToken, Task<Stream>> PhotoStream
        {
            get { return _photoStream; }
            set { SetProperty(ref _photoStream, value); }
        }

        private MvxAsyncCommand _profileCommand;
        public IMvxAsyncCommand ProfileCommand
        {
            get
            {
                return _profileCommand ?? (_profileCommand = new MvxAsyncCommand(DoProfileCommandAsync));
            }
        }


        private MvxAsyncCommand _exitCommand;
        private readonly IMvxNavigationService _navigationService;
        private readonly ISecureStorage _secureStorage;

        public IMvxAsyncCommand ExitCommand
        {
            get
            {
                return _exitCommand ?? (_exitCommand = new MvxAsyncCommand(DoExitCommandAsync));
            }
        }

        public TouristMenuViewModel(IMvxNavigationService navigationService, ISecureStorage secureStorage)
        {
            _navigationService = navigationService;
            _secureStorage = secureStorage;
        }

        public override void Prepare(TouristShortInfoDto parameter)
        {
            _title = parameter.Fullname;

            RaisePropertyChanged(nameof(Title));
        }

        private Task DoProfileCommandAsync()
        {
            throw new NotImplementedException();
        }

        private Task DoExitCommandAsync()
        {
            _secureStorage.RemoveAll();

            return _navigationService.Navigate<ChoiseUserRoleViewModel>();
        }
    }
}
