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
	[Register ("HotelNumberViewCell")]
	partial class HotelNumberViewCell
	{
		[Outlet]
		UIKit.UILabel _levelLabel { get; set; }

		[Outlet]
		UIKit.UILabel _numberLabel { get; set; }

		[Outlet]
		UIKit.UILabel _priceDescLabel { get; set; }

		[Outlet]
		UIKit.UILabel _priceLabel { get; set; }

		[Outlet]
		UIKit.UILabel _roomsAmountLabel { get; set; }

		[Outlet]
		UIKit.UILabel _visitorsAmountLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_levelLabel != null) {
				_levelLabel.Dispose ();
				_levelLabel = null;
			}

			if (_numberLabel != null) {
				_numberLabel.Dispose ();
				_numberLabel = null;
			}

			if (_priceDescLabel != null) {
				_priceDescLabel.Dispose ();
				_priceDescLabel = null;
			}

			if (_priceLabel != null) {
				_priceLabel.Dispose ();
				_priceLabel = null;
			}

			if (_roomsAmountLabel != null) {
				_roomsAmountLabel.Dispose ();
				_roomsAmountLabel = null;
			}

			if (_visitorsAmountLabel != null) {
				_visitorsAmountLabel.Dispose ();
				_visitorsAmountLabel = null;
			}
		}
	}
}
