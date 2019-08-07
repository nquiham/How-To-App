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
    [Register ("HowToCell")]
    partial class HowToCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel HowToDateLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel HowToDescriptionLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel HowToTitleLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (HowToDateLabel != null) {
                HowToDateLabel.Dispose ();
                HowToDateLabel = null;
            }

            if (HowToDescriptionLabel != null) {
                HowToDescriptionLabel.Dispose ();
                HowToDescriptionLabel = null;
            }

            if (HowToTitleLabel != null) {
                HowToTitleLabel.Dispose ();
                HowToTitleLabel = null;
            }
        }
    }
}