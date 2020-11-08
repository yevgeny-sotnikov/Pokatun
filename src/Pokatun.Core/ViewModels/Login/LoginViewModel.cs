using System;
using Pokatun.Core.Models.Enums;
using Pokatun.Core.Resources;

namespace Pokatun.Core.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel<UserRole>
    {
        private UserRole _role;

        public override string Title => Strings.EntranceToAccount;

        public override void Prepare(UserRole role)
        {
            _role = role;
        }
    }
}
