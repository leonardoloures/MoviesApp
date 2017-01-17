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
    [Register ("TableViewMovieCell")]
    partial class TableViewMovieCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel GenresText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel OverviewText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView PosterImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ReleaseDateImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ReleaseDateText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView StarImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel StarsText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TitleText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (GenresText != null) {
                GenresText.Dispose ();
                GenresText = null;
            }

            if (OverviewText != null) {
                OverviewText.Dispose ();
                OverviewText = null;
            }

            if (PosterImageView != null) {
                PosterImageView.Dispose ();
                PosterImageView = null;
            }

            if (ReleaseDateImageView != null) {
                ReleaseDateImageView.Dispose ();
                ReleaseDateImageView = null;
            }

            if (ReleaseDateText != null) {
                ReleaseDateText.Dispose ();
                ReleaseDateText = null;
            }

            if (StarImageView != null) {
                StarImageView.Dispose ();
                StarImageView = null;
            }

            if (StarsText != null) {
                StarsText.Dispose ();
                StarsText = null;
            }

            if (TitleText != null) {
                TitleText.Dispose ();
                TitleText = null;
            }
        }
    }
}