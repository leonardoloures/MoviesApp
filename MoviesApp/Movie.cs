using System;
using UIKit;

namespace MoviesApp
{
	// This class is the internal representation of a movie in MoviesApp
	public class Movie
	{
		public string Title { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string Overview { get; set; }
		public UIImage PosterImage { get; set; }
	}
}
