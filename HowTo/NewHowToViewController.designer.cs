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
    [Register ("NewHowToViewController")]
    partial class NewHowToViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView ModulesTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UINavigationItem NewHowToTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem NewTextBtn { get; set; }

        [Action ("UIBarButtonItem10417_Activated:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIBarButtonItem10417_Activated (UIKit.UIBarButtonItem sender);

        [Action ("UIBarButtonItem2686_Activated:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIBarButtonItem2686_Activated (UIKit.UIBarButtonItem sender);

        void ReleaseDesignerOutlets ()
        {
            if (ModulesTableView != null) {
                ModulesTableView.Dispose ();
                ModulesTableView = null;
            }

            if (NewHowToTitle != null) {
                NewHowToTitle.Dispose ();
                NewHowToTitle = null;
            }

            if (NewTextBtn != null) {
                NewTextBtn.Dispose ();
                NewTextBtn = null;
            }
        }
    }
}