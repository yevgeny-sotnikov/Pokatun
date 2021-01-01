// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Pokatun.iOS.Cells
{
	[Register ("LinkItemViewCell")]
	partial class LinkItemViewCell
	{
		[Outlet]
		Pokatun.iOS.Controls.BorderedTextField _linkTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_linkTextField != null) {
				_linkTextField.Dispose ();
				_linkTextField = null;
			}
		}
	}
}
