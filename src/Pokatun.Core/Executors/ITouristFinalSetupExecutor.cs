using System;
using System.Threading.Tasks;
using Pokatun.Core.ViewModels.Registration;
using Pokatun.Data;

namespace Pokatun.Core.Executors
{
    public interface ITouristFinalSetupExecutor
    {
        Task FinalizeSetupAsync(TokenInfoDto dto, TouristRegistrationViewModel touristRegistrationViewModel, bool v);
    }
}
