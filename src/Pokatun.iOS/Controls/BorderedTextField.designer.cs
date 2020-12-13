// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Pokatun.iOS.Controls
{
	partial class BorderedTextField
	{
		[Outlet]
		UIKit.UIImageView _leftImageView { get; set; }

		[Outlet]
		UIKit.UIButton _rightButton { get; set; }

		[Outlet]
		UIKit.UITextField _textField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_leftImageView != null) {
				_leftImageView.Dispose ();
				_leftImageView = null;
			}

			if (_rightButton != null) {
				_rightButton.Dispose ();
				_rightButton = null;
			}

			if (_textField != null) {
				_textField.Dispose ();
				_textField = null;
			}
		}
	}
}
