using System;
using UIKit;
using System.Collections.Generic;

namespace MoviesApp
{
	public class MoviesTableSource: UITableViewSource
	{
		private List<Movie> Movies;
		private string CellIdentifier = "TableCell";
		private UINavigationController NavigationController;

		public MoviesTableSource(List<Movie> movies, UINavigationController navigationController)
		{
			this.Movies = movies;
			this.NavigationController = navigationController;
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

		public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var movieController = this.NavigationController.Storyboard.InstantiateViewController("MovieController") as MovieController;

			if (movieController != null)
			{
				movieController.Movie = this.Movies[indexPath.Row];
				this.NavigationController.PushViewController(movieController, true);
			}
		}
	}
}
