// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Pokatun.iOS.Controls
{
	[Register ("TitlePhotoView")]
	partial class TitlePhotoView
	{
		[Outlet]
		UIKit.UIButton _addPhotoButton { get; set; }

		[Outlet]
		FFImageLoading.Cross.MvxCachedImageView _imageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_addPhotoButton != null) {
				_addPhotoButton.Dispose ();
				_addPhotoButton = null;
			}

			if (_imageView != null) {
				_imageView.Dispose ();
				_imageView = null;
			}
		}
	}
}
