using MvvmCross.ViewModels;

namespace Pokatun.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {

        public virtual string Title => null;
    }
}
