// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Pokatun.iOS.Views.Registration
{
	[Register ("HotelRegistrationSecondStepViewController")]
	partial class HotelRegistrationSecondStepViewController
	{
		[Outlet]
		UIKit.UITextField _bankCardOrIbanTextField { get; set; }

		[Outlet]
		UIKit.UITextField _bankNameTextField { get; set; }

		[Outlet]
		UIKit.UIButton _createAccountButton { get; set; }

		[Outlet]
		UIKit.UITextField _fullCompanyNameTextField { get; set; }

		[Outlet]
		UIKit.UITextField _usreouTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_bankCardOrIbanTextField != null) {
				_bankCardOrIbanTextField.Dispose ();
				_bankCardOrIbanTextField = null;
			}

			if (_bankNameTextField != null) {
				_bankNameTextField.Dispose ();
				_bankNameTextField = null;
			}

			if (_fullCompanyNameTextField != null) {
				_fullCompanyNameTextField.Dispose ();
				_fullCompanyNameTextField = null;
			}

			if (_usreouTextField != null) {
				_usreouTextField.Dispose ();
				_usreouTextField = null;
			}

			if (_createAccountButton != null) {
				_createAccountButton.Dispose ();
				_createAccountButton = null;
			}
		}
	}
}
