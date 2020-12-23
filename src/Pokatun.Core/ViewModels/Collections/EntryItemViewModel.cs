using System;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Pokatun.Core.ViewModels.Collections
{
    public sealed class EntryItemViewModel : MvxViewModel
    {
        private readonly IMvxCommand<EntryItemViewModel> _deleteItemCommand;

        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
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

        public EntryItemViewModel(string text, IMvxCommand<EntryItemViewModel> deleteItemCommand)
        {
            if (deleteItemCommand == null)
            {
                throw new ArgumentNullException(nameof(deleteItemCommand));
            }

            Text = text;

            _deleteItemCommand = deleteItemCommand;
        }
    }
}
