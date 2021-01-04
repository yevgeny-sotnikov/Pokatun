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
		Pokatun.iOS.Controls.Tabs.HotelProfileTab _hotelInfoTab { get; set; }

		[Outlet]
		Pokatun.iOS.Controls.Tabs.HotelProfileTab _personalDataTab { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_hotelInfoTab != null) {
				_hotelInfoTab.Dispose ();
				_hotelInfoTab = null;
			}

			if (_personalDataTab != null) {
				_personalDataTab.Dispose ();
				_personalDataTab = null;
			}
		}
	}
}
