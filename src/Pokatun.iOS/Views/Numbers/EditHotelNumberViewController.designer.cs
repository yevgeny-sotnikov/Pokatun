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
		Pokatun.iOS.Controls.BorderedButton _amountOfRoomsButton { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.BorderedButton _amountOfVisitorsButton { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.BorderedTextField _roomNumberTextField { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.BorderedButton _selectRoomLevelButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_roomNumberTextField != null) {
				_roomNumberTextField.Dispose ();
				_roomNumberTextField = null;
			}

			if (_selectRoomLevelButton != null) {
				_selectRoomLevelButton.Dispose ();
				_selectRoomLevelButton = null;
			}

			if (_amountOfRoomsButton != null) {
				_amountOfRoomsButton.Dispose ();
				_amountOfRoomsButton = null;
			}

			if (_amountOfVisitorsButton != null) {
				_amountOfVisitorsButton.Dispose ();
				_amountOfVisitorsButton = null;
			}
		}
	}
}
