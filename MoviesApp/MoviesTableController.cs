using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace MoviesApp
{
    public partial class MoviesTableController : UITableViewController
    {
        public MoviesTableController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var movies = MoviesDb.GetHardcodedMovies();

			TableView.Source = new MoviesTableSource(movies);
		}
    }
}