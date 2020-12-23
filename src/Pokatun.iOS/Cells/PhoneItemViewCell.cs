using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Collections;
using Pokatun.iOS.Controls;
using Pokatun.iOS.Styles;
using UIKit;

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

                var set = this.CreateBindingSet<PhoneItemViewCell, EntryItemViewModel>();

                #pragma warning restore IDE0008 // Use explicit type

                set.Bind(_phoneTextField).For(v => v.Text).To(vm => vm.Text).TwoWay();
                set.Bind(_phoneTextField).For(nameof(BorderedTextField.RightButtonClicked)).To(vm => vm.DeleteCommand).OneWay();

                set.Apply();
            });
        }
    }
}