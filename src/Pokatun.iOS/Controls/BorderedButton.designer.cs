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
	partial class BorderedButton
	{
		[Outlet]
		Pokatun.iOS.BorderView _borderView { get; set; }

		[Outlet]
		UIKit.UILabel _label { get; set; }

		[Outlet]
		UIKit.UIImageView _leftImageView { get; set; }

		[Outlet]
		UIKit.UIImageView _rightImageView { get; set; }

		[Action ("OnButtonTapped:")]
		partial void OnButtonTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (_borderView != null) {
				_borderView.Dispose ();
				_borderView = null;
			}

			if (_label != null) {
				_label.Dispose ();
				_label = null;
			}

			if (_leftImageView != null) {
				_leftImageView.Dispose ();
				_leftImageView = null;
			}

			if (_rightImageView != null) {
				_rightImageView.Dispose ();
				_rightImageView = null;
			}
		}
	}
}
