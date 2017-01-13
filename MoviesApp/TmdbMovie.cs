using System;
namespace MoviesApp
{
	// This class represents the TMDb movie returned in queries to the API
	public class TmdbMovie
	{
		public string title { get; set; }
		public DateTime release_date { get; set; }
		public string overview { get; set; }

		public Movie ToMovie()
		{
			var movie = new Movie()
			{
				Title = this.title,
				ReleaseDate = this.release_date,
				Overview = this.overview
			};

			return movie;
		}
	}
}
