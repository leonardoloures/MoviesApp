using System;
using System.Collections.Generic;

namespace MoviesApp
{
    // This class represents the TMDb response to genre/movie/list method in the API
    public class TmdbGenreMovieListResponse
    {
        public IList<TmdbGenre> genres;
    }
}
