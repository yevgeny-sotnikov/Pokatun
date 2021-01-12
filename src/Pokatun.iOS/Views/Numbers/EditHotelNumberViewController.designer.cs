// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Pokatun.iOS.Views.Numbers
{
	[Register ("EditHotelNumberViewController")]
	partial class EditHotelNumberViewController
	{
		[Outlet]
		Pokatun.iOS.Controls.BorderedTextField _roomNumberTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_roomNumberTextField != null) {
				_roomNumberTextField.Dispose ();
				_roomNumberTextField = null;
			}
		}
	}
}
