using Foundation;
using System;
using UIKit;

namespace MoviesApp
{
    public partial class TableViewMovieCell : UITableViewCell
    {
        public TableViewMovieCell (IntPtr handle) : base(handle)
        {
        }

        public TableViewMovieCell() : base()
        {
        }

        public void UpdateImage(UIImage image)
        {
            this.PosterImageView.Image = image;

            this.LayoutSubviews();
        }

        public void UpdateCell(string title, string releaseDate, string genres, string overview, string stars)
        {
            this.TitleText.Text = title;
            this.ReleaseDateText.Text = releaseDate;
            this.GenresText.Text = genres;
            this.OverviewText.Text = overview;
            this.StarsText.Text = stars;
            this.StarImageView.Hidden = stars == string.Empty;

            this.LayoutSubviews();
        }
    }
}