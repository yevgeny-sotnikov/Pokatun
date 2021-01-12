using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Numbers;
using UIKit;

namespace Pokatun.iOS.Views.Numbers
{
    [MvxModalPresentation(WrapInNavigationController = true, ModalPresentationStyle = UIModalPresentationStyle.BlurOverFullScreen)]
    public sealed partial class EditHotelNumberViewController : BaseViewController<EditHotelNumberViewModel>
    {
        public EditHotelNumberViewController() : base(nameof(EditHotelNumberViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            UIBarButtonItem rightBarButtonItem = new UIBarButtonItem { Image = UIImage.FromBundle("close") };
            NavigationItem.SetRightBarButtonItem(rightBarButtonItem, true);

            _roomNumberTextField.KeyboardType = UIKeyboardType.NumberPad;
            _roomNumberTextField.Placeholder = Strings.RoomNumber;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(NavigationItem.RightBarButtonItem).To(vm => vm.CloseCommand).OneTime();

            set.Bind(_roomNumberTextField).For(v => v.Text).To(vm => vm.Number).TwoWay();
            
            set.Apply();
        }
    }
}

