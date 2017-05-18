using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesApp
{
	// This class represents the TMDb movie returned in queries to the API
	public class TmdbMovie
	{
        public int id { get; set; }
		public string title { get; set; }
		public DateTime? release_date { get; set; }
		public string overview { get; set; }
		public string poster_path { get; set; }
        public IList<int> genre_ids { get; set; }
        public decimal vote_average { get; set; }
        public string original_title { get; set; }

        public Movie ToMovie(TmdbGenreMovieListResponse tmdbGenreMovieList)
		{
			var movie = new Movie()
			{
                Id = this.id,
				Title = this.title,
				ReleaseDate = this.release_date,
				Overview = this.overview,
                Genres = this.GetGenres(tmdbGenreMovieList),
                Stars = this.vote_average,
                OriginalTitle = this.original_title,
				PosterUrl = this.GetPosterUrl()
			};

			return movie;
		}

		private string GetPosterUrl()
		{
			// TODO: get these values dynamically
			const string secure_base_url = "https://image.tmdb.org/t/p/";
			const string poster_size = "w342";

			return secure_base_url + poster_size + this.poster_path;
		}

        private List<string> GetGenres(TmdbGenreMovieListResponse tmdbGenreMovieList)
        {
            var genres = new List<string>();
            foreach (var genreId in this.genre_ids)
            {
                try
                {
                    var genre = tmdbGenreMovieList.genres.Single(x => x.id == genreId).name;
                    genres.Add(genre);
                }
                catch
                {
                }
            }
            return genres;
        }
	}
}
