using System;
using UIKit;
using System.Threading.Tasks;

namespace MoviesApp
{
    public partial class MoviesTableController : UITableViewController
    {
		private MoviesTableSource MoviesTableSource;
		private int NextPage = 1;

        private UISearchController SearchController;

        public MoviesTableController (IntPtr handle) : base (handle)
        {
		}

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.MoviesTableSource = new MoviesTableSource(this);
			this.TableView.Source = this.MoviesTableSource;

			await this.LoadMoreMovies();

            this.ShowSearchBar();
        }

		public async Task LoadMoreMovies()
		{
			this.MoviesTableSource.StartLoading(this.TableView);

			var moreMovies = await Tmdb.GetUpcomingMovies(this.NextPage);
			if (moreMovies.Count > 0)
			{
				this.NextPage++;
			}

			this.MoviesTableSource.StopLoading(this.TableView);

			this.MoviesTableSource.AddMovies(moreMovies);
			this.TableView.ReloadData();
		}

        private void ShowSearchBar()
        {
            var searchResultsController = new SearchResultsViewController(this.MoviesTableSource.Movies, this.NavigationController);

            var searchUpdater = new SearchResultsUpdater();
            searchUpdater.UpdateSearchResults += searchResultsController.Search;

            this.SearchController = new UISearchController(searchResultsController)
            {
                SearchResultsUpdater = searchUpdater
            };

            this.SearchController.SearchBar.SizeToFit();
            this.SearchController.SearchBar.SearchBarStyle = UISearchBarStyle.Minimal;
            this.SearchController.SearchBar.Placeholder = "Search movie";

            this.SearchController.HidesNavigationBarDuringPresentation = true;
            this.DefinesPresentationContext = true;

            this.TableView.TableHeaderView = this.SearchController.SearchBar;
        }
    }
}