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
	[Register ("MenuItem")]
	partial class MenuItem
	{
		[Outlet]
		UIKit.UIImageView _menuItemIcon { get; set; }

		[Outlet]
		UIKit.UILabel _menuTextView { get; set; }

		[Outlet]
		UIKit.UIView _rootView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_rootView != null) {
				_rootView.Dispose ();
				_rootView = null;
			}

			if (_menuItemIcon != null) {
				_menuItemIcon.Dispose ();
				_menuItemIcon = null;
			}

			if (_menuTextView != null) {
				_menuTextView.Dispose ();
				_menuTextView = null;
			}
		}
	}
}
