using Foundation;
using System;
using UIKit;

namespace MoviesApp
{
    public partial class TabBarController : UITabBarController
    {
        private const int TabUpcomingMovies = 0;
        private const int TabAllMovies = 1;

        public TabBarController (IntPtr handle) : base (handle)
        {
            var upcomingMoviesNavigationController = this.ViewControllers[TabUpcomingMovies] as UINavigationController;
            var upcomingMovies = upcomingMoviesNavigationController.ViewControllers[0] as MoviesTableController;
            upcomingMovies.Title = "Upcoming";
            upcomingMovies.GetMovies = Tmdb.GetUpcomingMovies;

            var allMoviesNavigationController = this.ViewControllers[TabAllMovies] as UINavigationController;
            var allMovies = allMoviesNavigationController.ViewControllers[0] as MoviesTableController;
            allMovies.Title = "All";
            allMovies.GetMovies = Tmdb.GetAllMovies;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.TabBar.Items[TabUpcomingMovies].Title = "Upcoming";
            this.TabBar.Items[TabUpcomingMovies].Image = Resources.Calendar();

            this.TabBar.Items[TabAllMovies].Title = "All";
            this.TabBar.Items[TabAllMovies].Image = Resources.Star();
        }
    }
}