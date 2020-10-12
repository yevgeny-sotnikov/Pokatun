using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;

namespace Pokatun.Core.ViewModels
{
    public abstract class BaseViewModel<TParameter, TResult> : BaseViewModelResult<TResult>, IMvxViewModel<TParameter, TResult>
    {
        public abstract void Prepare(TParameter parameter);
    }
}
