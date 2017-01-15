using System;
using UIKit;
using System.Collections.Generic;

namespace MoviesApp
{
	public class MoviesTableSource: UITableViewSource
	{
		private List<Movie> Movies;
		private string MovieCellIdentifier = "MovieCell";
		private string LoadMoreCellIdentifier = "LoadMoreCell";

		private MoviesTableController MoviesTableController;
		private UINavigationController NavigationController;

		public MoviesTableSource(MoviesTableController moviesTableController)
		{
			this.Movies = new List<Movie>();

			this.MoviesTableController = moviesTableController;
			this.NavigationController = moviesTableController.NavigationController;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return this.Movies.Count + 1;
		}

		public void AddMovies(List<Movie> movies)
		{
			this.Movies.AddRange(movies);
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			if (this.IsMovieRow(indexPath))
			{
				var cell = tableView.DequeueReusableCell(this.MovieCellIdentifier, indexPath);
				if (cell == null)
				{
					cell = new UITableViewCell(UITableViewCellStyle.Subtitle, this.MovieCellIdentifier);
				}

				var movie = this.Movies[indexPath.Row];

				cell.TextLabel.Text = movie.Title;
				cell.DetailTextLabel.Text = movie.Overview;
				cell.ImageView.Image = movie.PosterImage;

				return cell;
			}
			else
			{
				var cell = tableView.DequeueReusableCell(this.LoadMoreCellIdentifier, indexPath);
				if (cell == null)
				{
					cell = new UITableViewCell(UITableViewCellStyle.Default, this.LoadMoreCellIdentifier);
				}

				return cell;
			}
		}

		public override nfloat GetHeightForRow(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			return this.IsMovieRow(indexPath) ? 100 : 44;
		}

		public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			if (this.IsMovieRow(indexPath))
			{
				var movieController = this.NavigationController.Storyboard.InstantiateViewController("MovieController") as MovieController;

				if (movieController != null)
				{
					movieController.Movie = this.Movies[indexPath.Row];
					this.NavigationController.PushViewController(movieController, true);
				}
			}
			else
			{
				this.MoviesTableController.LoadMoreMovies();
			}
		}

		private bool IsMovieRow(Foundation.NSIndexPath indexPath)
		{
			return indexPath.Row < this.Movies.Count;
		}
	}
}
