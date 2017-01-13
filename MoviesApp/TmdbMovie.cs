using System;
using Foundation;
using UIKit;

namespace MoviesApp
{
	// This class represents the TMDb movie returned in queries to the API
	public class TmdbMovie
	{
		public string title { get; set; }
		public DateTime release_date { get; set; }
		public string overview { get; set; }
		public string poster_path { get; set; }

		public Movie ToMovie()
		{
			var movie = new Movie()
			{
				Title = this.title,
				ReleaseDate = this.release_date,
				Overview = this.overview,
				PosterImage = this.GetPosterImage()
			};

			return movie;
		}

		private string GetPosterUrl()
		{
			// TODO: get these values dynamically
			const string secure_base_url = "https://image.tmdb.org/t/p/";
			const string poster_size = "w154";

			return secure_base_url + poster_size + this.poster_path;
		}

		private UIImage GetPosterImage()
		{
			using (var url = new NSUrl(this.GetPosterUrl()))
			using (var data = NSData.FromUrl(url))
			{
				if (data == null)
				{
					return UIImage.FromBundle("posterDefault.png");
				}
				else
				{
					return UIImage.LoadFromData(data);
				}
			}
		}
	}
}
