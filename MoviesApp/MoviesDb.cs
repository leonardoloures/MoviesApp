using System;
using System.Collections.Generic;

namespace MoviesApp
{
	public static class MoviesDb
	{
		public static List<Movie> GetHardcodedMovies()
		{
			var movies = new List<Movie>();

			movies.Add(new Movie("Shrek") { ReleaseDate = new DateTime(2004, 1, 1) });
			movies.Add(new Movie("Spider-Man") { ReleaseDate = new DateTime(2006, 1, 1) });
			movies.Add(new Movie("X-Men") { ReleaseDate = new DateTime(2007, 1, 1) });
			movies.Add(new Movie("Wolverine: Origins") { ReleaseDate = new DateTime(2010, 1, 1) });

			return movies;
		}
	}
}
