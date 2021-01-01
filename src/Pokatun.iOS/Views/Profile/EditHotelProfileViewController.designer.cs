// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Pokatun.iOS.Views.Profile
{
	[Register ("EditHotelProfileViewController")]
	partial class EditHotelProfileViewController
	{
		[Outlet]
		Pokatun.iOS.Controls.BorderedButton _addLinkButton { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.BorderedButton _addPhoneButton { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.BorderedTextField _bankCardOrIbanTextField { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.BorderedTextField _bankNameTextField { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.BorderedButton _checkInTimeButton { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.BorderedButton _checkOutTimeButton { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.BorderedTextField _emailTextField { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.BorderedTextField _fullCompanyNameTextField { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.MultilineCountableEditor _hotelDescriptionEditText { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.Tabs.HotelProfileTab _hotelInfoTab { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.BorderedButton _hotelLocationButton { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.BorderedTextField _hotelNameEditText { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.ResizableTableView _linksTable { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.Tabs.HotelProfileTab _personalDataTab { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.ResizableTableView _phonesTable { get; set; }

		[Outlet]
		UIKit.UIButton _saveChangesButton { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.BorderedTextField _usreouTextField { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.MultilineCountableEditor _withinTerritoryEditText { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_hotelDescriptionEditText != null) {
				_hotelDescriptionEditText.Dispose ();
				_hotelDescriptionEditText = null;
			}

			if (_withinTerritoryEditText != null) {
				_withinTerritoryEditText.Dispose ();
				_withinTerritoryEditText = null;
			}

			if (_addLinkButton != null) {
				_addLinkButton.Dispose ();
				_addLinkButton = null;
			}

			if (_addPhoneButton != null) {
				_addPhoneButton.Dispose ();
				_addPhoneButton = null;
			}

			if (_bankCardOrIbanTextField != null) {
				_bankCardOrIbanTextField.Dispose ();
				_bankCardOrIbanTextField = null;
			}

			if (_bankNameTextField != null) {
				_bankNameTextField.Dispose ();
				_bankNameTextField = null;
			}

			if (_checkInTimeButton != null) {
				_checkInTimeButton.Dispose ();
				_checkInTimeButton = null;
			}

			if (_checkOutTimeButton != null) {
				_checkOutTimeButton.Dispose ();
				_checkOutTimeButton = null;
			}

			if (_emailTextField != null) {
				_emailTextField.Dispose ();
				_emailTextField = null;
			}

			if (_fullCompanyNameTextField != null) {
				_fullCompanyNameTextField.Dispose ();
				_fullCompanyNameTextField = null;
			}

			if (_hotelInfoTab != null) {
				_hotelInfoTab.Dispose ();
				_hotelInfoTab = null;
			}

			if (_hotelLocationButton != null) {
				_hotelLocationButton.Dispose ();
				_hotelLocationButton = null;
			}

			if (_hotelNameEditText != null) {
				_hotelNameEditText.Dispose ();
				_hotelNameEditText = null;
			}

			if (_linksTable != null) {
				_linksTable.Dispose ();
				_linksTable = null;
			}

			if (_personalDataTab != null) {
				_personalDataTab.Dispose ();
				_personalDataTab = null;
			}

			if (_phonesTable != null) {
				_phonesTable.Dispose ();
				_phonesTable = null;
			}

			if (_saveChangesButton != null) {
				_saveChangesButton.Dispose ();
				_saveChangesButton = null;
			}

			if (_usreouTextField != null) {
				_usreouTextField.Dispose ();
				_usreouTextField = null;
			}
		}
	}
}
