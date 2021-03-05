using System.Collections.Generic;
using MvvmCross.Binding.BindingContext;
using Pokatun.Core.Converters;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Numbers;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.Numbers
{
    public partial class ShowHotelNumberViewController : BaseViewController<ShowHotelNumberViewModel>
    {
        public ShowHotelNumberViewController() : base(nameof(ShowHotelNumberViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            ViewTitle.IsSubtitleHidden = false;

            UIBarButtonItem rightBarButtonItem = new UIBarButtonItem { Image = UIImage.FromBundle("edit") };

            NavigationItem.SetRightBarButtonItem(rightBarButtonItem, true);

            _levelLabel.ApplyMediumLabelStyle();
            _roomsAmountLabel.ApplyMediumLabelStyle();
            _visitorsAmountLabel.ApplyMediumLabelStyle();

            _inNumberLabel.ApplyInfoLabelStyle();
            _descriptionLabel.ApplyAdditionalInfoLabelStyle();
            _cleaningLabel.ApplyInfoLabelStyle();
            _cleaningNeededLabel.ApplyAdditionalInfoLabelStyle();
            _nutritionLabel.ApplyInfoLabelStyle();
            _nutritionInfoLabel.ApplyAdditionalInfoLabelStyle();

            _submitBidButton.ApplyBigButtonStyle();
            _bidSubmittionDescriptionLabel.ApplyLargeLabelStyle();

            _inNumberLabel.Text = Strings.InHotelNumber;
            _cleaningLabel.Text = Strings.NumbersCleaning;
            _nutritionLabel.Text = Strings.Nutrition;
            _bidSubmittionDescriptionLabel.Text = Strings.BidSubmittionInfo;
            _submitBidButton.SetTitle(Strings.SubmitBid, UIControlState.Normal);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            set.Bind(ViewTitle).For(v => v.Subtitle).To(vm => vm.Subtitle).OneWay();
            set.Bind(rightBarButtonItem).To(vm => vm.EditCommand).OneTime();

            set.Bind(_levelLabel).To(vm => vm.HotelNumber.Level).WithConversion<RoomLevelToStringConverter>().OneWay();
            set.Bind(_roomsAmountLabel).To(vm => vm.HotelNumber.RoomsAmount).WithConversion<StringFormatValueConverter>(Strings.RoomsCounter).OneWay();
            set.Bind(_visitorsAmountLabel).To(vm => vm.HotelNumber.VisitorsAmount).WithConversion<StringFormatValueConverter>(Strings.VisitorsCounter).OneWay();

            set.Bind(_descriptionLabel).To(vm => vm.HotelNumber.Description).OneWay();
            set.Bind(_cleaningNeededLabel).To(vm => vm.HotelNumber.CleaningNeeded).WithDictionaryConversion(new Dictionary<bool, string>
            {
                { true, "Да" },
                { false, "Нет" }
            }).OneWay();
            set.Bind(_nutritionInfoLabel).To(vm => vm.NutritionInfo).OneWay();

            #pragma warning restore IDE0008 // Use explicit type

            set.Apply();
        }
    }
}

