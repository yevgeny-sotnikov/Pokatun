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
	[Register ("PhoneItemViewCell")]
	partial class PhoneItemViewCell
	{
		[Outlet]
		Pokatun.iOS.Controls.BorderedTextField _phoneTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_phoneTextField != null) {
				_phoneTextField.Dispose ();
				_phoneTextField = null;
			}
		}
	}
}
