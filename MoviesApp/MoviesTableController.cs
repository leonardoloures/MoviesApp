using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace MoviesApp
{
    public partial class MoviesTableController : UITableViewController
    {
		private MoviesTableSource MoviesTableSource;
		private int NextPage = 1;

        public MoviesTableController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.MoviesTableSource = new MoviesTableSource(this);
			this.TableView.Source = this.MoviesTableSource;

			this.LoadMoreMovies();
		}

		public void LoadMoreMovies()
		{
			var moreMovies = Tmdb.GetUpcomingMovies(this.NextPage);
			if (moreMovies.Count > 0)
			{
				this.NextPage++;
			}

			this.MoviesTableSource.AddMovies(moreMovies);
			this.TableView.ReloadData();
		}
    }
}