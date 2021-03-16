// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Pokatun.iOS.Views.Tourist
{
	[Register ("EditTouristProfileViewController")]
	partial class EditTouristProfileViewController
	{
		[Outlet]
		Pokatun.iOS.Controls.BorderedTextField _emailTextField { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.BorderedTextField _fullnameTextField { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.BorderedTextField _phoneTextField { get; set; }

		[Outlet]
		UIKit.UIButton _saveChangesButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_fullnameTextField != null) {
				_fullnameTextField.Dispose ();
				_fullnameTextField = null;
			}

			if (_phoneTextField != null) {
				_phoneTextField.Dispose ();
				_phoneTextField = null;
			}

			if (_emailTextField != null) {
				_emailTextField.Dispose ();
				_emailTextField = null;
			}

			if (_saveChangesButton != null) {
				_saveChangesButton.Dispose ();
				_saveChangesButton = null;
			}
		}
	}
}
