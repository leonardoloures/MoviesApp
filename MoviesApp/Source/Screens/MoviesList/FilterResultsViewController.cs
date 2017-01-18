using System;
using System.Collections.Generic;
using UIKit;

namespace MoviesApp
{
    public class FilterResultsViewController: UITableViewController
    {
        private const string ItemCellIdentifier = "ItemCellId";

        private List<Movie> AllMovies { get; set; }
        private List<Movie> MoviesFound { get; set; }

        private new UINavigationController NavigationController;

        public FilterResultsViewController(List<Movie> allMovies, UINavigationController navigationController)
        {
            this.AllMovies = allMovies;
            this.MoviesFound = new List<Movie>();
            this.NavigationController = navigationController;

            this.TableView.RegisterClassForCellReuse(typeof(TableViewMovieCell), ItemCellIdentifier);
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return this.MoviesFound.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(ItemCellIdentifier);
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, ItemCellIdentifier);
            }

            cell.TextLabel.Text = this.MoviesFound[indexPath.Row].Title;

            return cell;
        }

        public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var movieController = this.NavigationController.Storyboard.InstantiateViewController("MovieController") as MovieController;
            if (movieController != null)
            {
                movieController.Movie = this.MoviesFound[indexPath.Row];
                this.NavigationController.PushViewController(movieController, true);
            }
        }

        public void Filter(string forFilterString)
        {
            this.MoviesFound = this.AllMovies.FindAll(x => x.Title.IndexOf(forFilterString, StringComparison.OrdinalIgnoreCase) >= 0);
            this.TableView.ReloadData();
        }
    }
}
