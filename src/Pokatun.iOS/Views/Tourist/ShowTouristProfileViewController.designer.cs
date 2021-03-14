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
	[Register ("ShowTouristProfileViewController")]
	partial class ShowTouristProfileViewController
	{
		[Outlet]
		UIKit.UILabel _emailLabel { get; set; }

		[Outlet]
		UIKit.UILabel _fullnameLabel { get; set; }

		[Outlet]
		UIKit.UILabel _phoneLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_fullnameLabel != null) {
				_fullnameLabel.Dispose ();
				_fullnameLabel = null;
			}

			if (_emailLabel != null) {
				_emailLabel.Dispose ();
				_emailLabel = null;
			}

			if (_phoneLabel != null) {
				_phoneLabel.Dispose ();
				_phoneLabel = null;
			}
		}
	}
}
