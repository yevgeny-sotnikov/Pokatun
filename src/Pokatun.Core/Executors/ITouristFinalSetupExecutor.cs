using System.Threading.Tasks;
using MvvmCross.ViewModels;
using Pokatun.Data;

namespace Pokatun.Core.Executors
{
    public interface ITouristFinalSetupExecutor
    {
        Task FinalizeSetupAsync(TokenInfoDto dto, IMvxViewModel closeViewModel);
    }
}
