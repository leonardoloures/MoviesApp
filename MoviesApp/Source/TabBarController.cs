using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace MoviesApp
{
    public partial class TabBarController : UITabBarController
    {
        private const int TabUpcomingMovies = 0;
        private const int TabAllMovies = 1;
        private const int TabSearchMovies = 2;
        private const int TabSettings = 3;

        public TabBarController (IntPtr handle) : base (handle)
        {
            var upcomingMoviesNavigationController = this.ViewControllers[TabUpcomingMovies] as UINavigationController;
            var upcomingMoviesController = upcomingMoviesNavigationController.ViewControllers[0] as MoviesTableController;
            upcomingMoviesController.GetMovies = Tmdb.GetUpcomingMovies;
            upcomingMoviesController.LoadFirstPageAutomatically = true;
            upcomingMoviesController.EnableFilterBar = true;
            upcomingMoviesController.EnableSearchBar = false;

            var allMoviesNavigationController = this.ViewControllers[TabAllMovies] as UINavigationController;
            var allMoviesController = allMoviesNavigationController.ViewControllers[0] as MoviesTableController;
            allMoviesController.GetMovies = Tmdb.GetAllMovies;
            allMoviesController.LoadFirstPageAutomatically = true;
            allMoviesController.EnableFilterBar = true;
            allMoviesController.EnableSearchBar = false;

            var searchMoviesNavigationController = this.ViewControllers[TabSearchMovies] as UINavigationController;
            var searchMoviesController = searchMoviesNavigationController.ViewControllers[0] as MoviesTableController;
            searchMoviesController.GetMovies = Tmdb.SearchMovies;
            searchMoviesController.LoadFirstPageAutomatically = false;
            searchMoviesController.EnableFilterBar = false;
            searchMoviesController.EnableSearchBar = true;

            var settingsNavigationController = this.ViewControllers[TabSettings] as UINavigationController;
            var settingsTableController = settingsNavigationController.ViewControllers[0] as SettingsTableController;
            settingsTableController.RefreshAppLanguage = this.RefreshLanguage;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.TabBar.Items[TabUpcomingMovies].Image = Resources.Calendar();
            this.TabBar.Items[TabAllMovies].Image = Resources.Star();
            this.TabBar.Items[TabSearchMovies].Image = Resources.Search();

            this.RefreshLanguage();
        }

        public void RefreshLanguage()
        {
            this.TabBar.Items[TabUpcomingMovies].Title = Resources.LocalizedString("TabBar.Upcoming");
            this.TabBar.Items[TabAllMovies].Title = Resources.LocalizedString("TabBar.All");
            this.TabBar.Items[TabSearchMovies].Title = Resources.LocalizedString("TabBar.Search");
            this.TabBar.Items[TabSettings].Title = Resources.LocalizedString("TabBar.Settings");

            var upcomingMoviesNavigationController = this.ViewControllers[TabUpcomingMovies] as UINavigationController;
            var upcomingMoviesController = upcomingMoviesNavigationController.ViewControllers[0] as MoviesTableController;
            upcomingMoviesController.Title = Resources.LocalizedString("TabBar.Upcoming");
            upcomingMoviesController.RefreshLanguage();

            var allMoviesNavigationController = this.ViewControllers[TabAllMovies] as UINavigationController;
            var allMoviesController = allMoviesNavigationController.ViewControllers[0] as MoviesTableController;
            allMoviesController.Title = Resources.LocalizedString("TabBar.All");
            allMoviesController.RefreshLanguage();

            var searchMoviesNavigationController = this.ViewControllers[TabSearchMovies] as UINavigationController;
            var searchMoviesController = searchMoviesNavigationController.ViewControllers[0] as MoviesTableController;
            searchMoviesController.Title = Resources.LocalizedString("TabBar.Search");
            searchMoviesController.RefreshLanguage();

            var settingsNavigationController = this.ViewControllers[TabSettings] as UINavigationController;
            var settingsTableController = settingsNavigationController.ViewControllers[0] as SettingsTableController;
            settingsTableController.Title = Resources.LocalizedString("TabBar.Settings");
            settingsTableController.RefreshLanguage();

        }
    }
}