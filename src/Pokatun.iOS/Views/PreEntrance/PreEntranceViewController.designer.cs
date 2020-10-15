// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Pokatun.iOS.Views.PreEntrance
{
	[Register ("PreEntranceViewController")]
	partial class PreEntranceViewController
	{
		[Outlet]
		UIKit.UILabel _helloLabel { get; set; }

		[Outlet]
		UIKit.UILabel _iHaveAccLabel { get; set; }

		[Outlet]
		UIKit.UILabel _iStillDoesntHaveAccLabel { get; set; }

		[Outlet]
		UIKit.UIButton _loginButton { get; set; }

		[Outlet]
		UIKit.UILabel _preEntranceDescriptionLabel { get; set; }

		[Outlet]
		UIKit.UIButton _registrationButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_helloLabel != null) {
				_helloLabel.Dispose ();
				_helloLabel = null;
			}

			if (_iStillDoesntHaveAccLabel != null) {
				_iStillDoesntHaveAccLabel.Dispose ();
				_iStillDoesntHaveAccLabel = null;
			}

			if (_loginButton != null) {
				_loginButton.Dispose ();
				_loginButton = null;
			}

			if (_preEntranceDescriptionLabel != null) {
				_preEntranceDescriptionLabel.Dispose ();
				_preEntranceDescriptionLabel = null;
			}

			if (_registrationButton != null) {
				_registrationButton.Dispose ();
				_registrationButton = null;
			}

			if (_iHaveAccLabel != null) {
				_iHaveAccLabel.Dispose ();
				_iHaveAccLabel = null;
			}
		}
	}
}
