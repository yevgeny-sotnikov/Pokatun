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
	[Register ("TitleView")]
	partial class TitleView
	{
		[Outlet]
		UIKit.UIImageView _logo { get; set; }

		[Outlet]
		UIKit.UILabel _subtitleLabel { get; set; }

		[Outlet]
		UIKit.UILabel _titleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_subtitleLabel != null) {
				_subtitleLabel.Dispose ();
				_subtitleLabel = null;
			}

			if (_titleLabel != null) {
				_titleLabel.Dispose ();
				_titleLabel = null;
			}

			if (_logo != null) {
				_logo.Dispose ();
				_logo = null;
			}
		}
	}
}
