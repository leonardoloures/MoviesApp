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
    [Register ("MovieController")]
    partial class MovieController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView CalendarImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel CastHeadingText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel GenresText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel OriginalTitleText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel OverviewHeadingText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel OverviewText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView PosterImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ReleaseDateText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView ScrollView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView StarsImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel StarsText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TitleText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CalendarImageView != null) {
                CalendarImageView.Dispose ();
                CalendarImageView = null;
            }

            if (CastHeadingText != null) {
                CastHeadingText.Dispose ();
                CastHeadingText = null;
            }

            if (GenresText != null) {
                GenresText.Dispose ();
                GenresText = null;
            }

            if (OriginalTitleText != null) {
                OriginalTitleText.Dispose ();
                OriginalTitleText = null;
            }

            if (OverviewHeadingText != null) {
                OverviewHeadingText.Dispose ();
                OverviewHeadingText = null;
            }

            if (OverviewText != null) {
                OverviewText.Dispose ();
                OverviewText = null;
            }

            if (PosterImageView != null) {
                PosterImageView.Dispose ();
                PosterImageView = null;
            }

            if (ReleaseDateText != null) {
                ReleaseDateText.Dispose ();
                ReleaseDateText = null;
            }

            if (ScrollView != null) {
                ScrollView.Dispose ();
                ScrollView = null;
            }

            if (StarsImageView != null) {
                StarsImageView.Dispose ();
                StarsImageView = null;
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