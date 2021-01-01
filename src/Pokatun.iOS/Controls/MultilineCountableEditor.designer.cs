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
	[Register ("MultilineCountableEditor")]
	partial class MultilineCountableEditor
	{
		[Outlet]
		Pokatun.iOS.BorderView _borderView { get; set; }

		[Outlet]
		UIKit.UILabel _counterLabel { get; set; }

		[Outlet]
		UIKit.UITextView _dataTextField { get; set; }

		[Outlet]
		UIKit.UILabel _hintLabel { get; set; }

		[Outlet]
		UIKit.UILabel _titleLabel { get; set; }

		[Action ("OnWrapperAreaTapped:")]
		partial void OnWrapperAreaTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (_borderView != null) {
				_borderView.Dispose ();
				_borderView = null;
			}

			if (_counterLabel != null) {
				_counterLabel.Dispose ();
				_counterLabel = null;
			}

			if (_dataTextField != null) {
				_dataTextField.Dispose ();
				_dataTextField = null;
			}

			if (_hintLabel != null) {
				_hintLabel.Dispose ();
				_hintLabel = null;
			}

			if (_titleLabel != null) {
				_titleLabel.Dispose ();
				_titleLabel = null;
			}
		}
	}
}
