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
	[Register ("ShowHotelProfileViewController")]
	partial class ShowHotelProfileViewController
	{
		[Outlet]
		UIKit.UILabel _bankCardOrIbanLabel { get; set; }

		[Outlet]
		UIKit.UILabel _bankNameLabel { get; set; }

		[Outlet]
		UIKit.UILabel _emailLabel { get; set; }

		[Outlet]
		UIKit.UILabel _fullCompanyNameLabel { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.Tabs.HotelProfileTab _hotelInfoTab { get; set; }

		[Outlet]
		UIKit.UILabel _hotelLocationLabel { get; set; }

		[Outlet]
		UIKit.UILabel _hotelNameLabel { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.Tabs.HotelProfileTab _personalDataTab { get; set; }

		[Outlet]
		UIKit.UILabel _usreouLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_bankCardOrIbanLabel != null) {
				_bankCardOrIbanLabel.Dispose ();
				_bankCardOrIbanLabel = null;
			}

			if (_bankNameLabel != null) {
				_bankNameLabel.Dispose ();
				_bankNameLabel = null;
			}

			if (_emailLabel != null) {
				_emailLabel.Dispose ();
				_emailLabel = null;
			}

			if (_fullCompanyNameLabel != null) {
				_fullCompanyNameLabel.Dispose ();
				_fullCompanyNameLabel = null;
			}

			if (_hotelInfoTab != null) {
				_hotelInfoTab.Dispose ();
				_hotelInfoTab = null;
			}

			if (_hotelLocationLabel != null) {
				_hotelLocationLabel.Dispose ();
				_hotelLocationLabel = null;
			}

			if (_hotelNameLabel != null) {
				_hotelNameLabel.Dispose ();
				_hotelNameLabel = null;
			}

			if (_personalDataTab != null) {
				_personalDataTab.Dispose ();
				_personalDataTab = null;
			}

			if (_usreouLabel != null) {
				_usreouLabel.Dispose ();
				_usreouLabel = null;
			}
		}
	}
}
