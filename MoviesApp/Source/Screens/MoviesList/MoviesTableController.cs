using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;

namespace MoviesApp
{
    public partial class MoviesTableController : UITableViewController
    {
        public delegate Task<MoviesResponse> GetMoviesDelegate(int page, List<Tuple<string, string>> parameters);
        public GetMoviesDelegate GetMovies { get; set; }
        public List<Tuple<string, string>> GetMoviesParameters { get; set; }
        public bool LoadFirstPageAutomatically { get; set; }
        public bool EnableFilterBar { get; set; }
        public bool EnableSearchBar { get; set; }

        private MoviesTableSource MoviesTableSource;
        private int NextPage = 1;

        private UISearchController FilterController;
        private UISearchController SearchController;

        public MoviesTableController (IntPtr handle) : base (handle)
        {
            this.GetMoviesParameters = new List<Tuple<string, string>>();
		}

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.MoviesTableSource = new MoviesTableSource(this);
			this.TableView.Source = this.MoviesTableSource;

            if (this.EnableFilterBar)
            {
                this.ShowFilterBar();
            }

            if (this.EnableSearchBar)
            {
                this.ShowSearchBar();
            }

            if (this.LoadFirstPageAutomatically)
            {
                await this.LoadMoreMovies();
            }
            else
            {
                this.MoviesTableSource.EnableLoadingCell(this.TableView, false);
            }
        }

		public async Task LoadMoreMovies()
		{
            this.MoviesTableSource.EnableLoadingCell(this.TableView, true);
			this.MoviesTableSource.StartLoading(this.TableView);

            var moviesResponse = await GetMovies(this.NextPage, this.GetMoviesParameters);
			if (moviesResponse.Movies.Count > 0)
			{
				this.NextPage++;
			}

			this.MoviesTableSource.StopLoading(this.TableView);
            this.MoviesTableSource.EnableLoadingCell(this.TableView, moviesResponse.TotalPages >= this.NextPage);

            this.MoviesTableSource.AddMovies(moviesResponse.Movies);
			this.TableView.ReloadData();
		}

        private void ShowFilterBar()
        {
            var filterResultsController = new FilterResultsViewController(this.MoviesTableSource.Movies, this.NavigationController);

            var filterUpdater = new FilterResultsUpdater();
            filterUpdater.UpdateSearchResults += filterResultsController.Filter;

            this.FilterController = new UISearchController(filterResultsController)
            {
                SearchResultsUpdater = filterUpdater
            };

            this.FilterController.SearchBar.SizeToFit();
            this.FilterController.SearchBar.SearchBarStyle = UISearchBarStyle.Minimal;
            this.FilterController.SearchBar.Placeholder = Resources.LocalizedString("MoviesList.FilterMovie");

            this.FilterController.HidesNavigationBarDuringPresentation = true;
            this.DefinesPresentationContext = true;

            this.TableView.TableHeaderView = this.FilterController.SearchBar;
        }

        private void ShowSearchBar()
        {
            var searchMoviesController = new SearchMoviesViewController(this.NavigationController);

            var searchMoviesUpdater = new SearchMoviesUpdater();
            searchMoviesUpdater.UpdateSearchResults += searchMoviesController.Search;

            this.SearchController = new UISearchController(searchMoviesController)
            {
                SearchResultsUpdater = searchMoviesUpdater
            };

            this.SearchController.SearchBar.SizeToFit();
            this.SearchController.SearchBar.SearchBarStyle = UISearchBarStyle.Minimal;
            this.SearchController.SearchBar.Placeholder = Resources.LocalizedString("MoviesList.SearchMovie");

            this.SearchController.HidesNavigationBarDuringPresentation = false;
            this.DefinesPresentationContext = true;

            this.NavigationItem.TitleView = this.SearchController.SearchBar;

            this.SearchController.SearchBar.SearchButtonClicked += (sender, e) =>
            {
                this.DismissViewController(true, new Action(() =>
                {
                    this.SearchMovies(this.SearchController.SearchBar.Text);
                }));
            };

            this.SearchController.SearchBar.CancelButtonClicked += (sender, e) =>
            {
                this.ClearMovies();
            };

            this.SearchController.SearchBar.TextChanged += (sender, e) =>
            {
                if (this.SearchController.SearchBar.Text == string.Empty)
                {
                    this.ClearMovies();
                }
            };
        }

        private void SearchMovies(string query)
        {
            this.GetMovies = Tmdb.SearchMovies;
            this.GetMoviesParameters = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("query", query)
            };

            this.ClearMovies();
            this.LoadMoreMovies();
        }

        public void ClearMovies()
        {
            this.NextPage = 1;
            if (this.MoviesTableSource != null)
            {
                this.MoviesTableSource.Movies.Clear();
                this.TableView.ReloadData();
            }
        }

        public void RefreshLanguage()
        {
            this.ClearMovies();

            if (this.FilterController != null)
            {
                this.FilterController.SearchBar.Placeholder = Resources.LocalizedString("MoviesList.FilterMovie");
            }

            if (this.SearchController != null)
            {
                this.SearchController.SearchBar.Placeholder = Resources.LocalizedString("MoviesList.SearchMovie");
            }
        }
    }
}