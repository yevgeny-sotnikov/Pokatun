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
	[Register ("HotelRegistrationFirstStepViewController")]
	partial class HotelRegistrationFirstStepViewController
	{
		[Outlet]
		UIKit.UITextField _confirmPasswordEditText { get; set; }

		[Outlet]
		UIKit.UITextField _emailEditText { get; set; }

		[Outlet]
		UIKit.UIButton _furtherButton { get; set; }

		[Outlet]
		UIKit.UITextField _hotelNameEditText { get; set; }

		[Outlet]
		UIKit.UITextField _passwordEditText { get; set; }

		[Outlet]
		UIKit.UITextField _phoneNumberEditText { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_confirmPasswordEditText != null) {
				_confirmPasswordEditText.Dispose ();
				_confirmPasswordEditText = null;
			}

			if (_emailEditText != null) {
				_emailEditText.Dispose ();
				_emailEditText = null;
			}

			if (_furtherButton != null) {
				_furtherButton.Dispose ();
				_furtherButton = null;
			}

			if (_hotelNameEditText != null) {
				_hotelNameEditText.Dispose ();
				_hotelNameEditText = null;
			}

			if (_passwordEditText != null) {
				_passwordEditText.Dispose ();
				_passwordEditText = null;
			}

			if (_phoneNumberEditText != null) {
				_phoneNumberEditText.Dispose ();
				_phoneNumberEditText = null;
			}
		}
	}
}
