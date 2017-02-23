using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace MoviesApp
{
    public partial class MovieController : UIViewController
	{
		public Movie Movie { get; set; }

		private const int HorizontalMargin = 10;
		private const int VerticalMargin = 10;
		private const int VerticalSpacing = 5;
        private const int HorizontalSpacing = 5;

        private const int ActorViewWidth = 115;
        private const int ActorViewHeight = 180;
        private const int ActorViewHorizontalSpacing = 5;

		private nfloat LastBottom;

        private UIScrollView CastScrollView;
        private List<ActorView> CastViews;

		public MovieController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title = Resources.LocalizedString("MovieDetail.Title");
            this.OverviewHeadingText.Text = Resources.LocalizedString("MovieDetail.Overview");
            this.CastHeadingText.Text = Resources.LocalizedString("MovieDetail.Cast");

            this.TitleText.Text = this.Movie.Title;
            this.ReleaseDateText.Text = this.Movie.ReleaseDate?.ToString("d MMM yyyy");
            this.OverviewText.Text = this.Movie.Overview;
            this.StarsText.Text = this.Movie.Stars > 0 ? this.Movie.Stars.ToString() : string.Empty;
            this.StarsImageView.Hidden = this.Movie.Stars <= 0;
            this.GenresText.Text = this.Movie.GetGenres();
            this.OriginalTitleText.Text = string.Format("{0} ({1})",
                                                        this.Movie.OriginalTitle,
                                                        Resources.LocalizedString("MovieDetail.OriginalTitle"));

            this.CastScrollView = new UIScrollView();
            this.ScrollView.AddSubview(this.CastScrollView);

            this.Movie.GetPosterImage().ContinueWith(task => InvokeOnMainThread(() =>
            {
                this.PosterImageView.Image = task.Result;
            }));

            if (this.Movie.Cast == null)
            {
                Tmdb.GetCast(this.Movie.Id).ContinueWith(task => InvokeOnMainThread(() =>
                {
                    this.Movie.Cast = task.Result;
                    this.PopulateCastViews();
                }));
            }
            else
            {
                this.PopulateCastViews();
            }
        }
                                                                                    

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			this.LastBottom = MovieController.VerticalMargin;

            // Position calendar image, release date text, stars text and star image
            this.CalendarImageView.Frame = new CoreGraphics.CGRect(
                MovieController.HorizontalMargin,
                this.LastBottom,
                this.CalendarImageView.Bounds.Width,
                this.CalendarImageView.Bounds.Height);
            this.ReleaseDateText.Frame = new CoreGraphics.CGRect(
                this.CalendarImageView.Frame.Right + MovieController.HorizontalSpacing,
                this.LastBottom + 0.5 * (this.CalendarImageView.Bounds.Height - this.ReleaseDateText.Bounds.Height),
                this.ReleaseDateText.Bounds.Width,
                this.ReleaseDateText.Bounds.Height);
            this.StarsImageView.Frame = new CoreGraphics.CGRect(
                this.View.Bounds.Width - MovieController.HorizontalMargin - this.StarsImageView.Bounds.Width,
                this.LastBottom,
                this.StarsImageView.Bounds.Width,
                this.StarsImageView.Bounds.Height);
            this.StarsText.Frame = new CoreGraphics.CGRect(
                this.StarsImageView.Frame.Left - this.StarsText.Bounds.Width - MovieController.HorizontalSpacing,
                this.LastBottom + 0.5 * (this.StarsImageView.Bounds.Height - this.StarsText.Bounds.Height),
                this.StarsText.Bounds.Width,
                this.StarsText.Bounds.Height);
            this.LastBottom = this.CalendarImageView.Frame.Bottom;

			// Position TitleText
			this.PositionLabel(this.TitleText, MovieController.HorizontalMargin);

			// Position OriginalTitleText
            this.PositionLabel(this.OriginalTitleText, MovieController.HorizontalMargin);

            // Position GenresText
            this.PositionLabel(this.GenresText, MovieController.HorizontalMargin);

			// Position PosterImageView
            this.LastBottom += MovieController.VerticalMargin;
            this.PosterImageView.Frame = new CoreGraphics.CGRect(
				MovieController.HorizontalMargin,
				this.LastBottom + MovieController.VerticalSpacing,
				this.PosterImageView.Bounds.Width,
				this.PosterImageView.Bounds.Height);
			this.LastBottom = this.PosterImageView.Frame.Bottom;

            // Position OverviewHeadingText
            this.LastBottom += MovieController.VerticalMargin;
            this.PositionLabel(this.OverviewHeadingText, MovieController.HorizontalMargin);

            // Position OverviewText
			this.PositionLabel(this.OverviewText, MovieController.HorizontalMargin);

            // Position CastHeadingText
            this.PositionLabel(this.CastHeadingText, MovieController.HorizontalMargin);

            // Position cast scroll view
            this.PositionCastScrollView(MovieController.HorizontalMargin);

			this.ScrollView.ContentSize = new CoreGraphics.CGSize(this.View.Bounds.Width, this.LastBottom + MovieController.VerticalMargin);
		}

		private nfloat CalculateLabelHeight(UILabel label, nfloat width)
		{
			var text = new NSString(label.Text);
			var height = text.GetBoundingRect(
				new CoreGraphics.CGSize(width, float.MaxValue),
				NSStringDrawingOptions.UsesLineFragmentOrigin,
				new UIStringAttributes() { Font = label.Font },
				null).Height;
			
			return height;
		}

		private void PositionLabel(UILabel label, nfloat X)
		{
			var width = this.View.Bounds.Width - X - MovieController.HorizontalMargin;
			var height = this.CalculateLabelHeight(label, width);
			label.Frame = new CoreGraphics.CGRect(
				X,
				this.LastBottom + MovieController.VerticalSpacing,
				width,
				height);

			this.LastBottom = label.Frame.Bottom;
		}

        private void PositionCastScrollView(nfloat X)
        {
            if (this.CastViews == null)
            {
                return;
            }

            var width = this.View.Bounds.Width - X - MovieController.HorizontalMargin;
            this.CastScrollView.Frame = new CoreGraphics.CGRect(
                X,
                this.LastBottom + MovieController.VerticalSpacing,
                width,
                MovieController.ActorViewHeight);

            int i = 0;
            foreach (var actorView in this.CastViews)
            {
                actorView.Frame = new CoreGraphics.CGRect(
                    i * (MovieController.ActorViewWidth + MovieController.ActorViewHorizontalSpacing),
                    0,
                    MovieController.ActorViewWidth,
                    MovieController.ActorViewHeight);
                actorView.LayoutSubviews();
                i++;
            }

            this.CastScrollView.BackgroundColor = UIColor.White;

            this.CastScrollView.ContentSize = new CoreGraphics.CGSize(
                i * MovieController.ActorViewWidth + (i - 1) * MovieController.ActorViewHorizontalSpacing,
                MovieController.ActorViewHeight);
            this.LastBottom = this.CastScrollView.Frame.Bottom;
        }

        private void PopulateCastViews()
        {
            this.CastViews = new List<ActorView>();
            foreach (var actor in this.Movie.Cast)
            {
                var actorView = new ActorView(actor);
                this.CastViews.Add(actorView);
                this.CastScrollView.AddSubview(actorView);
            }
            this.View.LayoutSubviews();
        }
    }
}