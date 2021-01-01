using System;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Pokatun.Core.ViewModels.Collections
{
    public sealed class EntryItemViewModel : MvxViewModel
    {
        private readonly IMvxCommand<EntryItemViewModel> _deleteItemCommand;
        private readonly Func<string, bool> _validator;

        private string _text;
        private bool _viewInEditMode = true;

        public long Id { get; private set; }

        public string Text
        {
            get { return _text; }
            set
            {
                if (!SetProperty(ref _text, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsInvalid));
            }
        }

        public bool IsInvalid => !_viewInEditMode && _validator(Text);

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

        public EntryItemViewModel(long id, string text, IMvxCommand<EntryItemViewModel> deleteItemCommand, Func<string, bool> validator)
        {
            if (deleteItemCommand == null)
            {
                throw new ArgumentNullException(nameof(deleteItemCommand));
            }

            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            Id = id;

            Text = text;

            _deleteItemCommand = deleteItemCommand;
            _validator = validator;
        }

        public void Validate()
        {
            _viewInEditMode = false;

            RaisePropertyChanged(nameof(IsInvalid));
        }
    }
}
