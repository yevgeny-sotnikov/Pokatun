using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using Pokatun.Data;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Cells
{
    public partial class AddressItemViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString(nameof(AddressItemViewCell));
        public static readonly UINib Nib;

        static AddressItemViewCell()
        {
            Nib = UINib.FromName(nameof(AddressItemViewCell), NSBundle.MainBundle);
        }

        protected AddressItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            _addressLabel.ApplyLargeLabelStyle();

            this.DelayBind(() =>
            {
                #pragma warning disable IDE0008 // Use explicit type

                var set = this.CreateBindingSet<AddressItemViewCell, LocationDto>();

                #pragma warning restore IDE0008 // Use explicit type

                set.Bind(_addressLabel).To(vm => vm.Addres).OneWay();

                set.Apply();
            });
        }
    }
}

