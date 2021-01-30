// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Pokatun.iOS.Views.Profile
{
	[Register ("HotelAddressViewController")]
	partial class HotelAddressViewController
	{
		[Outlet]
		UIKit.UITableView _foundResultsTable { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.BorderedTextField _searchTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_searchTextField != null) {
				_searchTextField.Dispose ();
				_searchTextField = null;
			}

			if (_foundResultsTable != null) {
				_foundResultsTable.Dispose ();
				_foundResultsTable = null;
			}
		}
	}
}
