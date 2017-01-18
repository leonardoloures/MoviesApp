using System;
using System.Collections.Generic;
using UIKit;

namespace MoviesApp
{
    public class SearchMoviesViewController: UITableViewController
    {
        private string ItemCellIdentifier = "SearchCellIdentifier";
        private new UINavigationController NavigationController;
        private List<Movie> Movies;
        private bool Searching;
        private string LastStringToSearch;

        public SearchMoviesViewController(UINavigationController navigationController)
        {
            this.NavigationController = navigationController;
            this.Movies = new List<Movie>();
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return this.Movies.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(ItemCellIdentifier);
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, ItemCellIdentifier);
            }

            cell.TextLabel.Text = this.Movies[indexPath.Row].Title;

            return cell;
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

        public void Search(string toSearch)
        {
            this.LastStringToSearch = toSearch;

            if (toSearch == string.Empty)
            {
                this.Movies.Clear();
                this.TableView.ReloadData();
                return;
            }

            if (this.Searching == true)
            {
                return;
            }

            var parameters = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("query", toSearch)
            };

            this.Searching = true;
            Tmdb.SearchMovies(1, parameters).ContinueWith(task => InvokeOnMainThread(() =>
            {
                this.Movies = task.Result.Movies;
                this.TableView.ReloadData();
                this.Searching = false;

                if (this.LastStringToSearch != toSearch)
                {
                    this.Search(this.LastStringToSearch);
                }
            }));
        }
    }
}
