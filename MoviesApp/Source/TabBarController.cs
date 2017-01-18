using System;
using System.Collections.Generic;
using UIKit;

namespace MoviesApp
{
    public partial class TabBarController : UITabBarController
    {
        private const int TabUpcomingMovies = 0;
        private const int TabAllMovies = 1;
        private const int TabSearchMovies = 2;

        public TabBarController (IntPtr handle) : base (handle)
        {
            var upcomingMoviesNavigationController = this.ViewControllers[TabUpcomingMovies] as UINavigationController;
            var upcomingMoviesController = upcomingMoviesNavigationController.ViewControllers[0] as MoviesTableController;
            upcomingMoviesController.Title = "Upcoming";
            upcomingMoviesController.GetMovies = Tmdb.GetUpcomingMovies;
            upcomingMoviesController.LoadFirstPageAutomatically = true;
            upcomingMoviesController.EnableFilterBar = true;
            upcomingMoviesController.EnableSearchBar = false;

            var allMoviesNavigationController = this.ViewControllers[TabAllMovies] as UINavigationController;
            var allMoviesController = allMoviesNavigationController.ViewControllers[0] as MoviesTableController;
            allMoviesController.Title = "All";
            allMoviesController.GetMovies = Tmdb.GetAllMovies;
            allMoviesController.LoadFirstPageAutomatically = true;
            allMoviesController.EnableFilterBar = true;
            allMoviesController.EnableSearchBar = false;

            var searchMoviesNavigationController = this.ViewControllers[TabSearchMovies] as UINavigationController;
            var searchMoviesController = searchMoviesNavigationController.ViewControllers[0] as MoviesTableController;
            searchMoviesController.Title = "Search";
            searchMoviesController.GetMovies = Tmdb.SearchMovies;
            searchMoviesController.GetMoviesParameters = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("query", "TODO")
            };
            searchMoviesController.LoadFirstPageAutomatically = false;
            searchMoviesController.EnableFilterBar = false;
            searchMoviesController.EnableSearchBar = true;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.TabBar.Items[TabUpcomingMovies].Title = "Upcoming";
            this.TabBar.Items[TabUpcomingMovies].Image = Resources.Calendar();

            this.TabBar.Items[TabAllMovies].Title = "All";
            this.TabBar.Items[TabAllMovies].Image = Resources.Star();

            this.TabBar.Items[TabSearchMovies].Title = "Search";
            this.TabBar.Items[TabSearchMovies].Image = Resources.Search();
        }
    }
}