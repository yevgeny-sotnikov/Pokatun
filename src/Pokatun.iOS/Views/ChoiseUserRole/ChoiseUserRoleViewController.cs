using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Pokatun.Core.Models.Enums;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.ChoiseUserRole;
using Pokatun.iOS.Controls;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.ChoiseUserRole
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public sealed partial class ChoiseUserRoleViewController : BaseViewController<ChoiseUserRoleViewModel>
    {
        public ChoiseUserRoleViewController() : base(nameof(ChoiseUserRoleViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            _chooseRoleLabel.Font = Fonts.HelveticaNeueCyrLightSubGigantic;
            _touristButton.Font = Fonts.HelveticaNeueCyrLightGigantic;
            _hotelButton.Font = Fonts.HelveticaNeueCyrLightGigantic;

            _chooseRoleLabel.Text = Strings.ChooseRoleText;
            _hotelDescriptionLabel.Text = Strings.HotelRoleDescriptionText;
            _touristDescriptionLabel.Text = Strings.TouristRoleDescriptionText;
            _touristButton.SetTitle(Strings.Tourist, UIControlState.Normal);
            _hotelButton.SetTitle(Strings.Hotel, UIControlState.Normal);

            MvxFluentBindingDescriptionSet<IMvxIosView<ChoiseUserRoleViewModel>, ChoiseUserRoleViewModel> set = CreateBindingSet();

            set.Bind(_touristButton).To(vm => vm.RoleChoosedCommand).CommandParameter(UserRole.Tourist);
            set.Bind(_hotelButton).To(vm => vm.RoleChoosedCommand).CommandParameter(UserRole.HotelAdministrator);

            set.Apply();
        }
    }   
}

