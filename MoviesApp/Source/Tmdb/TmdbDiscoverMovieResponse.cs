﻿using System.Collections.Generic;

namespace MoviesApp
{
	// This class represents the TMDb response to discover/movie method in the API
	public class TmdbDiscoverMovieResponse
	{
		public int page { get; set; }
		public IList<TmdbMovie> results;
        public int total_pages { get; set; }

        public List<Movie> ToMovieList(TmdbGenreMovieListResponse tmdbGenreMovieListResponse)
		{
			var movies = new List<Movie>();

			foreach (var tmdbMovie in this.results)
			{
                movies.Add(tmdbMovie.ToMovie(tmdbGenreMovieListResponse));
			}

			return movies;
		}
	}
}
