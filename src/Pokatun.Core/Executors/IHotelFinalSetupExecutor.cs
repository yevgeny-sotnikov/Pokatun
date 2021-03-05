using System.Threading.Tasks;
using MvvmCross.ViewModels;
using Pokatun.Data;

namespace Pokatun.Core.Executors
{
    public interface IHotelFinalSetupExecutor
    {
        Task FinalizeSetupAsync(TokenInfoDto dto, IMvxViewModel closeViewModel);
    }
}
