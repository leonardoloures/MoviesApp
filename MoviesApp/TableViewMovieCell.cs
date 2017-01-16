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

        public void UpdateCell(string title, string releaseDate, string genres, string overview)
        {
            this.TitleText.Text = title;
            this.ReleaseDateText.Text = releaseDate;
            this.GenresText.Text = genres;
            this.OverviewText.Text = overview;

            this.LayoutSubviews();
        }
    }
}