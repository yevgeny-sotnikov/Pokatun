using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Converters;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Profile;
using Pokatun.iOS.Cells;
using Pokatun.iOS.Controls;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.Profile
{
    [MvxModalPresentation(WrapInNavigationController = true, ModalPresentationStyle = UIModalPresentationStyle.BlurOverFullScreen)]
    public partial class EditHotelProfileViewController : BaseViewController<EditHotelProfileViewModel>
    {
        private MvxSimpleTableViewSource _phonesTableViewSource;
        private MvxSimpleTableViewSource _linksTableViewSource;

        public EditHotelProfileViewController() : base(nameof(EditHotelProfileViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            UIBarButtonItem rightBarButtonItem = new UIBarButtonItem();
            rightBarButtonItem.Image = UIImage.FromBundle("close");

            NavigationItem.SetRightBarButtonItem(rightBarButtonItem, false);

            _phonesTableViewSource = CreateTableViewSource(_phonesTable, PhoneItemViewCell.Key);
            _linksTableViewSource = CreateTableViewSource(_linksTable, LinkItemViewCell.Key);

            _phonesTable.Source = _phonesTableViewSource;
            _linksTable.Source = _linksTableViewSource;

            _saveChangesButton.ApplyBigButtonStyle();

            _hotelNameEditText.ApplyBorderedEditTextStyle();
            _fullCompanyNameTextField.ApplyBorderedEditTextStyle();
            _emailTextField.ApplyBorderedEditTextStyle();
            _bankCardOrIbanTextField.ApplyBorderedEditTextStyle();
            _bankNameTextField.ApplyBorderedEditTextStyle();
            _usreouTextField.ApplyBorderedEditTextStyle();

            _addPhoneButton.ApplyBorderedButtonStyle();
            _hotelLocationButton.ApplyBorderedButtonStyle();
            _checkInTimeButton.ApplyBorderedButtonStyle();
            _checkOutTimeButton.ApplyBorderedButtonStyle();
            _addLinkButton.ApplyBorderedButtonStyle();

            _hotelNameEditText.KeyboardType = UIKeyboardType.Default;
            _fullCompanyNameTextField.KeyboardType = UIKeyboardType.Default;
            _emailTextField.KeyboardType = UIKeyboardType.EmailAddress;
            _bankCardOrIbanTextField.KeyboardType = UIKeyboardType.NamePhonePad;
            _bankNameTextField.KeyboardType = UIKeyboardType.Default;
            _usreouTextField.KeyboardType = UIKeyboardType.NumberPad;

            _hotelNameEditText.Placeholder = Strings.Name;
            _fullCompanyNameTextField.Placeholder = Strings.FullCompanyName;
            _emailTextField.Placeholder = Strings.Email;
            _bankCardOrIbanTextField.Placeholder = Strings.CardNumberOrIBAN;
            _bankNameTextField.Placeholder = Strings.BankName;
            _usreouTextField.Placeholder = Strings.USREOU;

            _withinTerritoryEditText.Title = Strings.WithinTerritory;
            _withinTerritoryEditText.Hint = Strings.WithinTerritoryFillingInstruction;
            _hotelDescriptionEditText.Title = Strings.Description;
            _hotelDescriptionEditText.Hint = Strings.DescriptionFillingInstruction;

            _addPhoneButton.Text = Strings.AddPhone;
            _hotelLocationButton.Text = Strings.HotelLocationAddress;
            _addLinkButton.Text = Strings.AddLink;

            _personalDataTab.Text = Strings.PersonalData;
            _hotelInfoTab.Text = Strings.HotelInfo;
            _saveChangesButton.SetTitle(Strings.SaveChanges, UIControlState.Normal);

            _withinTerritoryEditText.MaxLenght = 200;
            _hotelDescriptionEditText.MaxLenght = 600;

            #pragma warning disable IDE0008 // Use explicit type
    
            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(NavigationItem.RightBarButtonItem).To(vm => vm.CloseCommand).OneTime();

            set.Bind(_hotelNameEditText).For(v => v.Text).To(vm => vm.HotelName).TwoWay();
            set.Bind(_fullCompanyNameTextField).For(v => v.Text).To(vm => vm.FullCompanyName).TwoWay();
            set.Bind(_emailTextField).For(v => v.Text).To(vm => vm.Email).TwoWay();
            set.Bind(_bankCardOrIbanTextField).For(v => v.Text).To(vm => vm.BankCardOrIban).TwoWay();
            set.Bind(_bankNameTextField).For(v => v.Text).To(vm => vm.BankName).TwoWay();
            set.Bind(_usreouTextField).For(v => v.Text).To(vm => vm.USREOU).TwoWay();

            set.Bind(_withinTerritoryEditText).For(v => v.Data).To(vm => vm.WithinTerritoryDescription).TwoWay();
            set.Bind(_hotelDescriptionEditText).For(v => v.Data).To(vm => vm.HotelDescription).TwoWay();

            set.Bind(_checkInTimeButton).For(v => v.Text).To(vm => vm.CheckInTime).WithConversion<TimeConverter>(Strings.CheckInTime).OneWay();
            set.Bind(_checkOutTimeButton).For(v => v.Text).To(vm => vm.CheckOutTime).WithConversion<TimeConverter>(Strings.CheckOutTime).OneWay();

            set.Bind(_phonesTableViewSource).To(vm => vm.PhoneNumbers).OneTime();
            set.Bind(_linksTableViewSource).To(vm => vm.SocialResources).OneTime();

            set.Bind(_addPhoneButton).For(nameof(BorderedButton.Clicked)).To(vm => vm.AddPhoneCommand).OneTime();
            set.Bind(_addLinkButton).For(nameof(BorderedButton.Clicked)).To(vm => vm.AddSocialResourceCommand).OneTime();
            set.Bind(_checkInTimeButton).For(nameof(BorderedButton.Clicked)).To(vm => vm.ChooseCheckInTimeCommand).OneTime();
            set.Bind(_checkOutTimeButton).For(nameof(BorderedButton.Clicked)).To(vm => vm.ChooseCheckOutTimeCommand).OneTime();

            set.Bind(_hotelNameEditText).For(v => v.Highlighted).To(vm => vm.IsHotelNameInvalid).OneWay();
            set.Bind(_fullCompanyNameTextField).For(v => v.Highlighted).To(vm => vm.IsFullCompanyNameInvalid).OneWay();
            set.Bind(_emailTextField).For(v => v.Highlighted).To(vm => vm.IsEmailInvalid).OneWay();
            set.Bind(_bankCardOrIbanTextField).For(v => v.Highlighted).To(vm => vm.IsBankCardOrIbanInvalid).OneWay();
            set.Bind(_bankNameTextField).For(v => v.Highlighted).To(vm => vm.IsBankNameInvalid).OneWay();
            set.Bind(_usreouTextField).For(v => v.Highlighted).To(vm => vm.IsUsreouInvalid).OneWay();

            set.Apply();
        }

        private MvxSimpleTableViewSource CreateTableViewSource(UITableView tableView, string nibName)
        {
            return new MvxSimpleTableViewSource(tableView, nibName)
            {
                UseAnimations = true,
                RemoveAnimation = UITableViewRowAnimation.Right,
                AddAnimation = UITableViewRowAnimation.Fade
            };
        }
    }
}

