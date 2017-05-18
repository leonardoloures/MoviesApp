// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MoviesApp
{
    [Register ("TableViewLoadingCell")]
    partial class TableViewLoadingCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView ActivityIndicatorView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TitleText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ActivityIndicatorView != null) {
                ActivityIndicatorView.Dispose ();
                ActivityIndicatorView = null;
            }

            if (TitleText != null) {
                TitleText.Dispose ();
                TitleText = null;
            }
        }
    }
}