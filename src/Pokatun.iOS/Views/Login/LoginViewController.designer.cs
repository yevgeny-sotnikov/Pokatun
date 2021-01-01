// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Pokatun.iOS.Views.Login
{
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		Pokatun.iOS.Controls.BorderedTextField _emailTextField { get; set; }

		[Outlet]
		UIKit.UIButton _forgotPasswordButton { get; set; }

		[Outlet]
		UIKit.UIButton _loginButton { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.BorderedTextField _passwordTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_emailTextField != null) {
				_emailTextField.Dispose ();
				_emailTextField = null;
			}

			if (_forgotPasswordButton != null) {
				_forgotPasswordButton.Dispose ();
				_forgotPasswordButton = null;
			}

			if (_loginButton != null) {
				_loginButton.Dispose ();
				_loginButton = null;
			}

			if (_passwordTextField != null) {
				_passwordTextField.Dispose ();
				_passwordTextField = null;
			}
		}
	}
}
