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
	[Register ("TouristMenuViewController")]
	partial class TouristMenuViewController
	{
		[Outlet]
		Pokatun.iOS.Controls.MenuItem _activeBidItem { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.MenuItem _bidsArchiveItem { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.CardView _cardView { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.MenuItem _exitItem { get; set; }

		[Outlet]
		UIKit.UIStackView _menuContainer { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.MenuItem _myRatesItem { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.MenuItem _paymentItem { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.MenuItem _profileItem { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.MenuItem _securityItem { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_activeBidItem != null) {
				_activeBidItem.Dispose ();
				_activeBidItem = null;
			}

			if (_exitItem != null) {
				_exitItem.Dispose ();
				_exitItem = null;
			}

			if (_bidsArchiveItem != null) {
				_bidsArchiveItem.Dispose ();
				_bidsArchiveItem = null;
			}

			if (_myRatesItem != null) {
				_myRatesItem.Dispose ();
				_myRatesItem = null;
			}

			if (_paymentItem != null) {
				_paymentItem.Dispose ();
				_paymentItem = null;
			}

			if (_profileItem != null) {
				_profileItem.Dispose ();
				_profileItem = null;
			}

			if (_securityItem != null) {
				_securityItem.Dispose ();
				_securityItem = null;
			}

			if (_cardView != null) {
				_cardView.Dispose ();
				_cardView = null;
			}

			if (_menuContainer != null) {
				_menuContainer.Dispose ();
				_menuContainer = null;
			}
		}
	}
}
