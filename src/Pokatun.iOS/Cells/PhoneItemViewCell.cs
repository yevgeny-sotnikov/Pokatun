using System;

using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;
using Pokatun.iOS.Styles;
using Pokatun.Core.Resources;
using MvvmCross.Binding.BindingContext;
using Pokatun.Core.ViewModels.Profile;
using Pokatun.iOS.Controls;

namespace Pokatun.iOS.Cells
{
    public sealed partial class PhoneItemViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString(nameof(PhoneItemViewCell));
        public static readonly UINib Nib;

        static PhoneItemViewCell()
        {
            Nib = UINib.FromName(nameof(PhoneItemViewCell), NSBundle.MainBundle);
        }

        public PhoneItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            _phoneTextField.ApplyBorderedEditTextStyle();

            _phoneTextField.Placeholder = Strings.EnterPhone;
            _phoneTextField.KeyboardType = UIKeyboardType.PhonePad;

            this.DelayBind(() =>
            {
                #pragma warning disable IDE0008 // Use explicit type

                var set = this.CreateBindingSet<PhoneItemViewCell, PhoneItemViewModel>();

                #pragma warning restore IDE0008 // Use explicit type

                set.Bind(_phoneTextField).For(v => v.Text).To(vm => vm.PhoneNumber).TwoWay();
                set.Bind(_phoneTextField).For(nameof(BorderedTextField.RightButtonClicked)).To(vm => vm.DeleteItemCommand).OneWay();

                set.Apply();
            });
        }
    }
}