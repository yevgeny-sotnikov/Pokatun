using MvvmCross.ViewModels;
using Pokatun.Core.Enums;

namespace Pokatun.Core.ViewModels.Collections
{
    public class EntryItemViewModel : MvxViewModel
    {
        public EntryType Type { get; set; }

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                SetProperty(ref _text, value);
            }
        }
    }
}
