using System;
using UIKit;
using System.Collections.Generic;

namespace MoviesApp
{
	public class MoviesTableSource: UITableViewSource
	{
		private List<Movie> Movies;
		private string CellIdentifier = "TableCell";

		public MoviesTableSource(List<Movie> movies)
		{
			this.Movies = movies;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return this.Movies.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(this.CellIdentifier, indexPath);
			if (cell == null)
			{
				cell = new UITableViewCell(UITableViewCellStyle.Subtitle, this.CellIdentifier);
			}

			var movie = this.Movies[indexPath.Row];

			cell.TextLabel.Text = movie.Title;
			cell.DetailTextLabel.Text = movie.Overview;
			cell.ImageView.Image = movie.PosterImage;

			return cell;
		}

		public override nfloat GetHeightForRow(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			return 100;
		}
	}
}
