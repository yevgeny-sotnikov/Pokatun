using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Converters;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Numbers;
using Pokatun.iOS.Controls;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.Numbers
{
    [MvxModalPresentation(WrapInNavigationController = true, ModalPresentationStyle = UIModalPresentationStyle.BlurOverFullScreen)]
    public sealed partial class EditHotelNumberViewController : BaseViewController<EditHotelNumberViewModel>
    {
        private const int DescriptionMaxLength = 200;

        public EditHotelNumberViewController() : base(nameof(EditHotelNumberViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            UIBarButtonItem rightBarButtonItem = new UIBarButtonItem { Image = UIImage.FromBundle("close") };
            NavigationItem.SetRightBarButtonItem(rightBarButtonItem, true);

            _saveChangesButton.ApplyBigButtonStyle();
            _amountOfRoomsButton.ApplyBorderedButtonStyle();
            _amountOfVisitorsButton.ApplyBorderedButtonStyle();
            _borderView.ApplyBorderViewStyle();
            _hintLabel.ApplyAdditionalInfoLabelStyle();
            _hintLabel.Font = Fonts.HelveticaNeueCyrLightExtraLarge;
            _cleaningLabel.ApplyExtraLargeLabelStyle();
            _cleaningNeededSwitch.ApplySwitchStyle();
            _nutritionNeededSwitch.ApplySwitchStyle();
            _breakfastCheckbox.ApplyCheckBoxStyle();
            _dinnerCheckbox.ApplyCheckBoxStyle();
            _supperCheckbox.ApplyCheckBoxStyle();
            _nutritionLabel.ApplyExtraLargeLabelStyle();

            _numberDescriptionTextField.Font = Fonts.HelveticaNeueCyrLightExtraLarge;
            _numberDescriptionTextField.TextColor = ColorPalette.PrimaryText;

            _numberDescriptionTextField.Started += OnDescriptionEditingStarted;
            _numberDescriptionTextField.Ended += OnDescriptionEditingEnded;
            _numberDescriptionTextField.Changed += OnTextChanged;
            _breakfastCheckbox.TouchUpInside += CheckBoxButtonTouchUpInside;
            _dinnerCheckbox.TouchUpInside += CheckBoxButtonTouchUpInside;
            _supperCheckbox.TouchUpInside += CheckBoxButtonTouchUpInside;

            _roomNumberTextField.KeyboardType = UIKeyboardType.NumberPad;
            _roomNumberTextField.Placeholder = Strings.RoomNumber;
            _hintLabel.Text = Strings.HotelNumberDescription;
            _cleaningLabel.Text = Strings.NumbersCleaning;
            _nutritionLabel.Text = Strings.Nutrition;
            _breakfastCheckbox.SetTitle(Strings.Breakfast, UIControlState.Normal);
            _dinnerCheckbox.SetTitle(Strings.Dinner, UIControlState.Normal);
            _supperCheckbox.SetTitle(Strings.Supper, UIControlState.Normal);
            _hotelNumberPriceTextField.Placeholder = Strings.HotelNumberPriceHint;
            _saveChangesButton.SetTitle(Strings.SaveChanges, UIControlState.Normal);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(NavigationItem.RightBarButtonItem).To(vm => vm.CloseCommand).OneTime();

            set.Bind(_roomNumberTextField).For(v => v.Text).To(vm => vm.Number).WithConversion<NullableValueConverter>().TwoWay();
            set.Bind(_numberDescriptionTextField).To(vm => vm.Description).TwoWay();
            set.Bind(_hotelNumberPriceTextField).For(v => v.Text).To(vm => vm.Price).WithConversion<NullableValueConverter>().TwoWay();

            set.Bind(_selectRoomLevelButton).For(v => v.Text).To(vm => vm.Level).WithConversion<RoomLevelToStringConverter>().OneWay();
            set.Bind(_amountOfRoomsButton).For(v => v.Text).To(vm => vm.RoomsAmount)
                .WithConversion<StringFormatValueConverter>(Strings.RoomsNumFormat).OneWay();
            set.Bind(_amountOfVisitorsButton).For(v => v.Text).To(vm => vm.VisitorsAmount)
                .WithConversion<StringFormatValueConverter>(Strings.PeopleNumFormat).OneWay();

            set.Bind(_cleaningNeededSwitch).For(v => v.On).To(vm => vm.CleaningNeeded).TwoWay();
            set.Bind(_nutritionNeededSwitch).For(v => v.On).To(vm => vm.NutritionNeeded).TwoWay();

            set.Bind(_breakfastCheckbox).For(v => v.Selected).To(vm => vm.BreakfastIncluded).TwoWay();
            set.Bind(_dinnerCheckbox).For(v => v.Selected).To(vm => vm.DinnerIncluded).TwoWay();
            set.Bind(_supperCheckbox).For(v => v.Selected).To(vm => vm.SupperIncluded).TwoWay();

            set.Bind(_breakfastCheckbox).For(v => v.UserInteractionEnabled).To(vm => vm.NutritionNeeded).OneWay();
            set.Bind(_dinnerCheckbox).For(v => v.UserInteractionEnabled).To(vm => vm.NutritionNeeded).OneWay();
            set.Bind(_supperCheckbox).For(v => v.UserInteractionEnabled).To(vm => vm.NutritionNeeded).OneWay();

            IDictionary<bool, nfloat> alphaConversionDictionary = new Dictionary<bool, nfloat> { { true, 1f }, { false, 0.5f } };

            set.Bind(_breakfastCheckbox).For(v => v.Alpha).To(vm => vm.NutritionNeeded).WithDictionaryConversion(alphaConversionDictionary).OneWay();
            set.Bind(_dinnerCheckbox).For(v => v.Alpha).To(vm => vm.NutritionNeeded).WithDictionaryConversion(alphaConversionDictionary).OneWay();
            set.Bind(_supperCheckbox).For(v => v.Alpha).To(vm => vm.NutritionNeeded).WithDictionaryConversion(alphaConversionDictionary).OneWay();

            set.Bind(_roomNumberTextField).For(v => v.Highlighted).To(vm => vm.IsNumberInvalid).OneWay();

            set.Bind(_borderView).For(v => v.Highlighted).To(vm => vm.IsDescriptionInvalid).OneWay();
            set.Bind(_hintLabel).For(v => v.TextColor).To(vm => vm.IsDescriptionInvalid).WithDictionaryConversion(
                new Dictionary<bool, UIColor>
                {
                    { true, ColorPalette.FailValidationColor },
                    { false, ColorPalette.PrimaryText }
                }
            ).OneWay();

            set.Bind(_hotelNumberPriceTextField).For(v => v.Highlighted).To(vm => vm.IsPriceInvalid).OneWay();

            set.Bind(_selectRoomLevelButton).For(nameof(BorderedButton.Clicked)).To(vm => vm.SelectRoomLevelCommand).OneTime();
            set.Bind(_amountOfRoomsButton).For(nameof(BorderedButton.Clicked)).To(vm => vm.PromptRoomsAmountCommand).OneTime();
            set.Bind(_amountOfVisitorsButton).For(nameof(BorderedButton.Clicked)).To(vm => vm.PromptVisitorsAmountCommand).OneTime();

            set.Bind(_saveChangesButton).To(vm => vm.SaveChangesCommand).OneTime();

            set.Apply();
        }

        private void CheckBoxButtonTouchUpInside(object sender, EventArgs e)
        {
            UICheckBox checkbox = (UICheckBox)sender;
            checkbox.Selected = !checkbox.Selected;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            _hintLabel.Hidden = _numberDescriptionTextField.Text.Length > 0;

            CenterTextViewText();
        }

        private void OnDescriptionEditingStarted(object sender, EventArgs e)
        {
            _borderView.InEditMode = true;

            _numberDescriptionTextField.TextColor = ColorPalette.PrimaryText;
            _hintLabel.TextColor = ColorPalette.BorderColor;

            CenterTextViewText();
        }

        private void CenterTextViewText()
        {
            double topCorrect = (_numberDescriptionTextField.Bounds.Size.Height - _numberDescriptionTextField.ContentSize.Height * _numberDescriptionTextField.ZoomScale) / 2.0;

            if (topCorrect < 0)
            {
                return;
            }

            _numberDescriptionTextField.ContentOffset = new CGPoint(0, -topCorrect);
        }

        private void OnDescriptionEditingEnded(object sender, EventArgs e)
        {
            _borderView.InEditMode = false;
        }

        private bool OnDescriptionShouldChangeText(UITextView textView, NSRange range, string text)
        {
            nint newLength = textView.Text.Length + text.Length - range.Length;

            return newLength <= DescriptionMaxLength;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _numberDescriptionTextField.Started -= OnDescriptionEditingStarted;
                _numberDescriptionTextField.Ended -= OnDescriptionEditingEnded;
                _numberDescriptionTextField.Changed -= OnTextChanged;
                _breakfastCheckbox.TouchUpInside += CheckBoxButtonTouchUpInside;
                _dinnerCheckbox.TouchUpInside += CheckBoxButtonTouchUpInside;
                _supperCheckbox.TouchUpInside += CheckBoxButtonTouchUpInside;
            }

            base.Dispose(disposing);
        }
    }
}

