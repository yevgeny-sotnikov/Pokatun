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
    public sealed partial class LinkItemViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString(nameof(LinkItemViewCell));
        public static readonly UINib Nib;

        static LinkItemViewCell()
        {
            Nib = UINib.FromName(nameof(LinkItemViewCell), NSBundle.MainBundle);
        }

        public LinkItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            _linkTextField.ApplyBorderedEditTextStyle();

            _linkTextField.Placeholder = Strings.SiteOrSocialNetworkLink;
            _linkTextField.KeyboardType = UIKeyboardType.Url;

            this.DelayBind(() =>
            {
                #pragma warning disable IDE0008 // Use explicit type

                var set = this.CreateBindingSet<LinkItemViewCell, EntryItemViewModel>();

                #pragma warning restore IDE0008 // Use explicit type

                set.Bind(_linkTextField).For(v => v.Text).To(vm => vm.Text).TwoWay();
                set.Bind(_linkTextField).For(v => v.Highlighted).To(vm => vm.IsInvalid).OneWay();
                set.Bind(_linkTextField).For(nameof(BorderedTextField.RightButtonClicked)).To(vm => vm.DeleteCommand).OneWay();

                set.Apply();
            });
        }

    }
}

