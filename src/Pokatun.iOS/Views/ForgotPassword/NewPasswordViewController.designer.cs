// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Pokatun.iOS.Views.ForgotPassword
{
	[Register ("NewPasswordViewController")]
	partial class NewPasswordViewController
	{
		[Outlet]
		UIKit.UITextField _confirmPasswordEditText { get; set; }

		[Outlet]
		UIKit.UITextField _passwordEditText { get; set; }

		[Outlet]
		UIKit.UIButton _saveButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_passwordEditText != null) {
				_passwordEditText.Dispose ();
				_passwordEditText = null;
			}

			if (_confirmPasswordEditText != null) {
				_confirmPasswordEditText.Dispose ();
				_confirmPasswordEditText = null;
			}

			if (_saveButton != null) {
				_saveButton.Dispose ();
				_saveButton = null;
			}
		}
	}
}
