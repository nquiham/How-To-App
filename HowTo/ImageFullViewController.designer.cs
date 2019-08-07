// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace HowTo
{
    [Register ("ImageFullViewController")]
    partial class ImageFullViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ModuleImage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ModuleImage != null) {
                ModuleImage.Dispose ();
                ModuleImage = null;
            }
        }
    }
}