using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using Pokatun.Core.ViewModels.Collections;
using Pokatun.iOS.Converters;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Cells
{
    public partial class ShowItemViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString(nameof(ShowItemViewCell));
        public static readonly UINib Nib;

        static ShowItemViewCell()
        {
            Nib = UINib.FromName(nameof(ShowItemViewCell), NSBundle.MainBundle);
        }

        public ShowItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            this.DelayBind(() =>
            {
                _textLabel.ApplyLargeLabelStyle();

                #pragma warning disable IDE0008 // Use explicit type

                var set = this.CreateBindingSet<ShowItemViewCell, EntryItemViewModel>();

                #pragma warning restore IDE0008 // Use explicit type
                
                set.Bind(_imageView).To(vm => vm.Type).WithConversion<EntityTypeToImageConverter>().OneWay();
                set.Bind(_textLabel).To(vm => vm.Text).OneWay();

                set.Apply();
            });
        }
    }
}

