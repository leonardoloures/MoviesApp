using Foundation;
using System;
using UIKit;

namespace MoviesApp
{
    public partial class MovieController : UIViewController
	{
		public Movie Movie { get; set; }

		private const int HorizontalMargin = 10;
		private const int VerticalMargin = 10;
		private const int VerticalSpacing = 10;
		private nfloat LastBottom;

		public MovieController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.TitleText.Text = this.Movie.Title;
			this.ReleaseDateText.Text = this.Movie.ReleaseDate.ToString("d MMM yyyy");
			this.OverviewText.Text = this.Movie.Overview;
			this.PosterImageView.Image = this.Movie.PosterImage;
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			this.LastBottom = MovieController.VerticalMargin;

			// Position TitleText
			this.PositionLabel(this.TitleText, MovieController.HorizontalMargin);

			// Position ReleaseDateText
			this.PositionLabel(this.ReleaseDateText, MovieController.HorizontalMargin);

			// Position PosterImageView
			this.PosterImageView.Frame = new CoreGraphics.CGRect(
				MovieController.HorizontalMargin,
				this.LastBottom + MovieController.VerticalSpacing,
				this.PosterImageView.Bounds.Width,
				this.PosterImageView.Bounds.Height);
			this.LastBottom = this.PosterImageView.Frame.Bottom;

			// Position OverviewText
			this.PositionLabel(this.OverviewText, MovieController.HorizontalMargin);

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
    }
}