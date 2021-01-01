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
	[Register ("VerificationCodeViewController")]
	partial class VerificationCodeViewController
	{
		[Outlet]
		UIKit.UIButton _button { get; set; }

		[Outlet]
		UIKit.UILabel _descriptionLabel { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.BorderedTextField _textField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_button != null) {
				_button.Dispose ();
				_button = null;
			}

			if (_descriptionLabel != null) {
				_descriptionLabel.Dispose ();
				_descriptionLabel = null;
			}

			if (_textField != null) {
				_textField.Dispose ();
				_textField = null;
			}
		}
	}
}
