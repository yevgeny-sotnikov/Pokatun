using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Converters;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Numbers;
using Pokatun.iOS.Controls;
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
            //_numberDescriptionTextField.Hint = Strings.HotelNumberDescription;
            //_cleaningLabel.Text = Strings.NumbersCleaning;
            //_nutritionLabel.Text = Strings.Nutrition;
            //_breakfastCheckbox.Text = Strings.Breakfast;
            //_dinnerCheckbox.Text = Strings.Dinner;
            //_supperCheckbox.Text = Strings.Supper;
            //_hotelNumberPriceTextField.Hint = Strings.HotelNumberPriceHint;
            //_saveChangesButton.Text = Strings.SaveChanges;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(NavigationItem.RightBarButtonItem).To(vm => vm.CloseCommand).OneTime();

            set.Bind(_roomNumberTextField).For(v => v.Text).To(vm => vm.Number).WithConversion<NullableValueConverter>().TwoWay();

            set.Bind(_selectRoomLevelButton).For(v => v.Text).To(vm => vm.Level).WithConversion<RoomLevelToStringConverter>().OneWay();
            set.Bind(_amountOfRoomsButton).For(v => v.Text).To(vm => vm.RoomsAmount)
                .WithConversion<StringFormatValueConverter>(Strings.RoomsNumFormat).OneWay();
            set.Bind(_amountOfVisitorsButton).For(v => v.Text).To(vm => vm.VisitorsAmount)
                .WithConversion<StringFormatValueConverter>(Strings.PeopleNumFormat).OneWay();

            set.Bind(_selectRoomLevelButton).For(nameof(BorderedButton.Clicked)).To(vm => vm.SelectRoomLevelCommand).OneTime();
            set.Bind(_amountOfRoomsButton).For(nameof(BorderedButton.Clicked)).To(vm => vm.PromptRoomsAmountCommand).OneTime();
            set.Bind(_amountOfVisitorsButton).For(nameof(BorderedButton.Clicked)).To(vm => vm.PromptVisitorsAmountCommand).OneTime();

            set.Apply();
        }
    }
}

