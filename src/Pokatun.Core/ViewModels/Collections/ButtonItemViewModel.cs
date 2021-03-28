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

        private readonly Func<ButtonItemViewModel, bool> _validator;
        private bool _viewInEditMode = true;


        private DateTime? _minDate;
        public DateTime? MinDate
        {
            get { return _minDate; }
            set
            {
                if (!SetProperty(ref _minDate, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsInvalid));
            }
        }

        private DateTime? _maxDate;
        public DateTime? MaxDate
        {
            get { return _maxDate; }
            set { SetProperty(ref _maxDate, value); }
        }

        public bool BothTimesSetted => MinDate != null && MaxDate != null;

        public bool IsInvalid => !_viewInEditMode && _validator(this);

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

        public ButtonItemViewModel(IMvxCommand<ButtonItemViewModel> deleteItemCommand, IUserDialogs userDialogs, Func<ButtonItemViewModel, bool> validator)
        {
            if (deleteItemCommand == null)
            {
                throw new ArgumentNullException(nameof(deleteItemCommand));
            }

            if (userDialogs == null)
            {
                throw new ArgumentNullException(nameof(userDialogs));
            }

            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            _deleteItemCommand = deleteItemCommand;
            _userDialogs = userDialogs;
            _validator = validator;
        }

        private async Task DoActionCommandAsync()
        {
            DatePromptResult minResult = await _userDialogs.DatePromptAsync(selectedDate: MinDate);

            if (!minResult.Ok)
            {
                return;
            }

            DatePromptResult maxResult = await _userDialogs.DatePromptAsync(new DatePromptConfig
            {
                SelectedDate = minResult.Value,
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

        public void Validate()
        {
            _viewInEditMode = false;

            RaisePropertyChanged(nameof(IsInvalid));
        }
    }
}
