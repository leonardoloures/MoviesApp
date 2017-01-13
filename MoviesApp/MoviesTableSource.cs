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

			cell.TextLabel.Text = this.Movies[indexPath.Row].Title;
			cell.DetailTextLabel.Text = this.Movies[indexPath.Row].Overview;

			return cell;
		}
	}
}
