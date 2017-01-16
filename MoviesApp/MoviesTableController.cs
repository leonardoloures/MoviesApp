using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesApp
{
    public partial class MoviesTableController : UITableViewController
    {
		private MoviesTableSource MoviesTableSource;
		private int NextPage = 1;

        public MoviesTableController (IntPtr handle) : base (handle)
        {
		}

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.MoviesTableSource = new MoviesTableSource(this);
			this.TableView.Source = this.MoviesTableSource;

			await this.LoadMoreMovies();
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
    }
}