using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace MoviesApp
{
    public class MoviesTableSource: UITableViewSource
    {
        private List<Movie> Movies;

        private string MovieCellIdentifier = "MovieCell";
        private string LoadingCellIdentifier = "LoadingCell";

        private MoviesTableController MoviesTableController;
        private UINavigationController NavigationController;

        private bool Loading = false;

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

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            if (this.IsMovieRow(indexPath))
            {
                return this.GetCellForMovie(tableView, indexPath);
            }
            else
            {
                return this.GetCellForLoading(tableView, indexPath);
            }
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return this.IsMovieRow(indexPath) ? 100 : 44;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if (this.IsMovieRow(indexPath))
            {
                this.MovieRowSelected(tableView, indexPath);
            }
            else
            {
                this.LoadingRowSelected(tableView, indexPath);
            }
        }

        public void StartLoading(UITableView tableView)
        {
            this.Loading = true;
            this.ReloadLoadingCell(tableView);
        }

        public void StopLoading(UITableView tableView)
        {
            this.Loading = false;
            this.ReloadLoadingCell(tableView);
        }

        private UITableViewCell GetCellForMovie(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(this.MovieCellIdentifier, indexPath) as TableViewMovieCell;
            if (cell == null)
            {
                cell = new TableViewMovieCell() as TableViewMovieCell;
            }

            var movie = this.Movies[indexPath.Row];

            cell.UpdateCell(movie.Title, movie.ReleaseDate.ToString("dd MMM yy"), "Drama, terror", movie.Overview);
            cell.UpdateImage(UIImage.FromBundle("posterDefault.png"));

            movie.GetPosterImage().ContinueWith(task => InvokeOnMainThread(() =>
            {
                cell.UpdateImage(task.Result);
            }));

            return cell;
        }

        private UITableViewCell GetCellForLoading(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(this.LoadingCellIdentifier, indexPath) as TableViewLoadingCell;
            if (cell == null)
            {
                cell = new TableViewLoadingCell(UITableViewCellStyle.Default, this.LoadingCellIdentifier);
            }

            if (this.Loading)
            {
                cell.StartAnimating();
            }
            else
            {
                cell.StopAnimating();
            }

            return cell;
        }

        private void MovieRowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var movieController = this.NavigationController.Storyboard.InstantiateViewController("MovieController") as MovieController;
            if (movieController != null)
            {
                movieController.Movie = this.Movies[indexPath.Row];
                this.NavigationController.PushViewController(movieController, true);
            }
        }

        private void LoadingRowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if (this.Loading == false)
            {
                this.MoviesTableController.LoadMoreMovies();
            }
        }


        private bool IsMovieRow(NSIndexPath indexPath)
        {
            return indexPath.Row < this.Movies.Count;
        }

        private void ReloadLoadingCell(UITableView tableView)
        {
            var rowsToReload = new NSIndexPath[]
            {
                NSIndexPath.FromRowSection(this.Movies.Count, 0)
            };
            tableView.ReloadRows(rowsToReload, UITableViewRowAnimation.None);
        }
    }
}
