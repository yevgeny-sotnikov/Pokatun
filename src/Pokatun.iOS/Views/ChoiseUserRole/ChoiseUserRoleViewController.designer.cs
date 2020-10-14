// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Pokatun.iOS.Views.ChoiseUserRole
{
	[Register ("ChoiseUserRoleViewController")]
	partial class ChoiseUserRoleViewController
	{
		[Outlet]
		UIKit.UILabel _chooseRoleLabel { get; set; }

		[Outlet]
		UIKit.UIButton _hotelButton { get; set; }

		[Outlet]
		UIKit.UILabel _hotelDescriptionLabel { get; set; }

		[Outlet]
		UIKit.UIButton _touristButton { get; set; }

		[Outlet]
		UIKit.UILabel _touristDescriptionLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_chooseRoleLabel != null) {
				_chooseRoleLabel.Dispose ();
				_chooseRoleLabel = null;
			}

			if (_touristButton != null) {
				_touristButton.Dispose ();
				_touristButton = null;
			}

			if (_hotelButton != null) {
				_hotelButton.Dispose ();
				_hotelButton = null;
			}

			if (_touristDescriptionLabel != null) {
				_touristDescriptionLabel.Dispose ();
				_touristDescriptionLabel = null;
			}

			if (_hotelDescriptionLabel != null) {
				_hotelDescriptionLabel.Dispose ();
				_hotelDescriptionLabel = null;
			}
		}
	}
}
