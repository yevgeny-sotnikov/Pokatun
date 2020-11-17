// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Pokatun.iOS.Views.Menu
{
	[Register ("HotelMenuViewController")]
	partial class HotelMenuViewController
	{
		[Outlet]
		Pokatun.iOS.Controls.MenuItem _conditionsAndLoyaltyProgramItem { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.MenuItem _exitItem { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.MenuItem _hotelRatingItem { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.MenuItem _myBidsItem { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.MenuItem _myHotelNumbersItem { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.MenuItem _profileItem { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.MenuItem _securityItem { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_myBidsItem != null) {
				_myBidsItem.Dispose ();
				_myBidsItem = null;
			}

			if (_myHotelNumbersItem != null) {
				_myHotelNumbersItem.Dispose ();
				_myHotelNumbersItem = null;
			}

			if (_hotelRatingItem != null) {
				_hotelRatingItem.Dispose ();
				_hotelRatingItem = null;
			}

			if (_profileItem != null) {
				_profileItem.Dispose ();
				_profileItem = null;
			}

			if (_conditionsAndLoyaltyProgramItem != null) {
				_conditionsAndLoyaltyProgramItem.Dispose ();
				_conditionsAndLoyaltyProgramItem = null;
			}

			if (_securityItem != null) {
				_securityItem.Dispose ();
				_securityItem = null;
			}

			if (_exitItem != null) {
				_exitItem.Dispose ();
				_exitItem = null;
			}
		}
	}
}
