using Pokatun.Core.Models.Enums;
using Pokatun.Core.Resources;

namespace Pokatun.Core.ViewModels.PreEntrance
{
    public sealed class PreEntranceViewModel : BaseViewModel<UserRole>
    {
        private UserRole _role;

        public string PreEntranceDescriptionText => _role == UserRole.Tourist
            ? Strings.PreEntranceTouristDescriptionText
            : Strings.PreEntranceHotelDescriptionText;

        public override void Prepare(UserRole role)
        {
            _role = role;
        }
    }
}
