using System;
using System.Threading.Tasks;
using MvvmCross.ViewModels;
using Pokatun.Core.ViewModels.Registration;
using Pokatun.Data;

namespace Pokatun.Core.Executors
{
    public interface ITouristFinalSetupExecutor
    {
        Task FinalizeSetupAsync(TokenInfoDto dto, IMvxViewModel closeViewModel, bool needLoadShortInfo = true);
    }
}
