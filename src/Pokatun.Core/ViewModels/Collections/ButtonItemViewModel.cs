using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Pokatun.Core.ViewModels.Collections
{
    public sealed class ButtonItemViewModel : MvxViewModel
    {
        private readonly IMvxCommand<ButtonItemViewModel> _deleteItemCommand;
        private readonly IUserDialogs _userDialogs;

        private DateTime? _minDate;
        public DateTime? MinDate
        {
            get { return _minDate; }
            set { SetProperty(ref _minDate, value); }
        }

        private DateTime? _maxDate;
        public DateTime? MaxDate
        {
            get { return _maxDate; }
            set { SetProperty(ref _maxDate, value); }
        }

        public bool BothTimesSetted => MinDate != null && MaxDate != null;

        private MvxAsyncCommand _actionCommand;
        public IMvxAsyncCommand ActionCommand
        {
            get
            {
                return _actionCommand ?? (_actionCommand = new MvxAsyncCommand(DoActionCommandAsync));
            }
        }

        private MvxCommand _deleteCommand;
        public IMvxCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new MvxCommand(
                    () => _deleteItemCommand.Execute(this),
                    () => _deleteItemCommand.CanExecute(this)
                ));
            }
        }


        public ButtonItemViewModel(IMvxCommand<ButtonItemViewModel> deleteItemCommand, IUserDialogs userDialogs)
        {
            if (deleteItemCommand == null)
            {
                throw new ArgumentNullException(nameof(deleteItemCommand));
            }

            if (userDialogs == null)
            {
                throw new ArgumentNullException(nameof(userDialogs));
            }

            _deleteItemCommand = deleteItemCommand;
            _userDialogs = userDialogs;
        }

        private async Task DoActionCommandAsync()
        {
            DatePromptResult minResult = await _userDialogs.DatePromptAsync(new DatePromptConfig
            {
                MaximumDate = MaxDate == null ? null : MaxDate
            });

            if (!minResult.Ok)
            {
                return;
            }


            DatePromptResult maxResult = await _userDialogs.DatePromptAsync(new DatePromptConfig
            {
                MinimumDate = MinDate == null ? null : MinDate
            });

            if (!maxResult.Ok)
            {
                return;
            }

            MinDate = minResult.Value;
            MaxDate = maxResult.Value;

            await RaisePropertyChanged(nameof(BothTimesSetted));
        }
    }
}
