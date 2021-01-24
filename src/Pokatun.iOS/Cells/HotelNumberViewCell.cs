using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using Pokatun.Core.Converters;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Collections;
using Pokatun.Data;
using Pokatun.iOS.Controls;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Cells
{
    public sealed partial class HotelNumberViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString(nameof(HotelNumberViewCell));
        public static readonly UINib Nib;

        static HotelNumberViewCell()
        {
            Nib = UINib.FromName(nameof(HotelNumberViewCell), NSBundle.MainBundle);
        }

        protected HotelNumberViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            _numberLabel.ApplyLargeBoldLabelStyle();
            _levelLabel.ApplyMediumLabelStyle();
            _roomsAmountLabel.ApplyMediumLabelStyle();
            _visitorsAmountLabel.ApplyMediumLabelStyle();
            _priceDescLabel.ApplyMediumLabelStyle();

            _priceLabel.ApplyMediumBoldLabelStyle();

            _priceDescLabel.Text = Strings.PriceHint;

            this.DelayBind(() =>
            {
                #pragma warning disable IDE0008 // Use explicit type

                var set = this.CreateBindingSet<HotelNumberViewCell, HotelNumberDto>();

                #pragma warning restore IDE0008 // Use explicit type

                set.Bind(_numberLabel).To(vm => vm.Number).WithConversion<StringFormatValueConverter>("№ {0}").OneWay();
                set.Bind(_levelLabel).To(vm => vm.Level).WithConversion<RoomLevelToStringConverter>().OneWay();
                set.Bind(_roomsAmountLabel).To(vm => vm.RoomsAmount).WithConversion<StringFormatValueConverter>("{0} ком").OneWay();
                set.Bind(_visitorsAmountLabel).To(vm => vm.VisitorsAmount).WithConversion<StringFormatValueConverter>("{0} чел").OneWay();
                set.Bind(_priceLabel).To(vm => vm.Price).OneWay();

                set.Apply();
            });

        }
    }
}

