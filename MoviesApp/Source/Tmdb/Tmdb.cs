using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MoviesApp
{
	// This class is responsible for querying TMDb API
	public static class Tmdb
	{
		private const string SecureBaseUrl = "https://api.themoviedb.org/3";
		private const string MethodDiscoverMovie = "/discover/movie";
        private const string MethodGenreMovieList = "/genre/movie/list";
        private const string MethodMovieCredits = "/movie/{0}/credits";
        private const string MethodSearchMovie = "/search/movie";

		private const string ParameterApiKey = "api_key";
		private const string ParameterPrimaryReleaseDateGreaterOrEqual = "primary_release_date.gte";
		private const string ParameterPage = "page";
        private const string ParameterSortBy = "sort_by";
        private const string ParameterQuery = "query";

        private const string ParameterValueApiKey = "1f54bd990f1cdfb230adb312546d765d";
        private const string ParameterValueSortByPrimaryReleaseDateAsc = "primary_release_date.asc";
        private const string ParameterValueSortByPrimaryReleaseDateDesc = "primary_release_date.desc";
        private const string ParameterValueSortByVoteAverageDesc = "vote_average.desc";

		public async static Task<List<Movie>> GetUpcomingMovies(int page, List<Tuple<string, string>> parameters)
		{
			var movies = new List<Movie>();

            var tmdbDiscoverMovieResponse = await Tmdb.CallDiscoverMovieUpcoming(page, parameters);
            var tmdbGenreMovieListResponse = await Tmdb.CallGenreMovieList();
            if (tmdbGenreMovieListResponse != null)
            {
                movies = tmdbDiscoverMovieResponse.ToMovieList(tmdbGenreMovieListResponse);
            }

			return movies;
		}

        public async static Task<List<Movie>> GetAllMovies(int page, List<Tuple<string, string>> parameters)
        {
            var movies = new List<Movie>();

            var tmdbDiscoverMovieResponse = await Tmdb.CallDiscoverMovieAll(page, parameters);
            var tmdbGenreMovieListResponse = await Tmdb.CallGenreMovieList();
            if (tmdbDiscoverMovieResponse != null)
            {
                movies = tmdbDiscoverMovieResponse.ToMovieList(tmdbGenreMovieListResponse);
            }

            return movies;
        }

        public async static Task<List<Movie>> SearchMovies(int page, List<Tuple<string, string>> parameters)
        {
            var movies = new List<Movie>();

            var tmdbDiscoverMovieResponse = await Tmdb.CallSearchMovie(page, parameters);
            var tmdbGenreMovieListResponse = await Tmdb.CallGenreMovieList();
            movies = tmdbDiscoverMovieResponse.ToMovieList(tmdbGenreMovieListResponse);

            return movies;
        }

        public async static Task<List<Actor>> GetCast(int movieId)
        {
            var cast = new List<Actor>();

            var tmdbMovieCreditsResponse = await Tmdb.CallMovieCredits(movieId);
            cast = tmdbMovieCreditsResponse.ToActorList();

            return cast;
        }

        private async static Task<TmdbDiscoverMovieResponse> CallDiscoverMovieUpcoming(int page, List<Tuple<string, string>> parameters)
		{
            parameters.Add(new Tuple<string, string>(Tmdb.ParameterApiKey, Tmdb.ParameterValueApiKey));
            parameters.Add(new Tuple<string, string>(Tmdb.ParameterPrimaryReleaseDateGreaterOrEqual, DateTime.Today.ToString("yyyy-MM-dd")));
            parameters.Add(new Tuple<string, string>(Tmdb.ParameterPage, page.ToString()));
            parameters.Add(new Tuple<string, string>(Tmdb.ParameterSortBy, Tmdb.ParameterValueSortByPrimaryReleaseDateAsc));

            var response = await Tmdb.CallMethod<TmdbDiscoverMovieResponse>(Tmdb.MethodDiscoverMovie, parameters);

			return response;
		}

        private async static Task<TmdbDiscoverMovieResponse> CallDiscoverMovieAll(int page, List<Tuple<string, string>> parameters)
        {
            parameters.Add(new Tuple<string, string>(Tmdb.ParameterApiKey, Tmdb.ParameterValueApiKey));
            parameters.Add(new Tuple<string, string>(Tmdb.ParameterPage, page.ToString()));
            parameters.Add(new Tuple<string, string>(Tmdb.ParameterSortBy, Tmdb.ParameterValueSortByVoteAverageDesc));

            var response = await Tmdb.CallMethod<TmdbDiscoverMovieResponse>(Tmdb.MethodDiscoverMovie, parameters);

            return response;
        }


        private async static Task<TmdbGenreMovieListResponse> CallGenreMovieList()
        {
            var parameters = new List<Tuple<string, string>>
            {
                        new Tuple<string, string>(Tmdb.ParameterApiKey, Tmdb.ParameterValueApiKey)
            };

            var response = await Tmdb.CallMethod<TmdbGenreMovieListResponse>(Tmdb.MethodGenreMovieList, parameters);

            return response;
        }

        private async static Task<TmdbMovieCreditsResponse> CallMovieCredits(int movieId)
        {
            var method = string.Format(Tmdb.MethodMovieCredits, movieId);
            var parameters = new List<Tuple<string, string>>
            {
                new Tuple<string, string>(Tmdb.ParameterApiKey, Tmdb.ParameterValueApiKey)
            };

            var response = await Tmdb.CallMethod<TmdbMovieCreditsResponse>(method, parameters);

            return response;
        }

        private async static Task<TmdbDiscoverMovieResponse> CallSearchMovie(int page, List<Tuple<string, string>> parameters)
        {
            parameters.Add(new Tuple<string, string>(Tmdb.ParameterApiKey, Tmdb.ParameterValueApiKey));
            parameters.Add(new Tuple<string, string>(Tmdb.ParameterPage, page.ToString()));
            parameters.Add(new Tuple<string, string>(Tmdb.ParameterSortBy, Tmdb.ParameterValueSortByPrimaryReleaseDateDesc));

            var response = await Tmdb.CallMethod<TmdbDiscoverMovieResponse>(Tmdb.MethodSearchMovie, parameters);

            return response;
        }

        private async static Task<T> CallMethod<T>(string method, List<Tuple<string, string>> parameters)
        {
            try
            {
                var request = Tmdb.CreateRequest(method, parameters);
                WebResponse response = await request.GetResponseAsync();
                var httpResponse = (HttpWebResponse)response;

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    Stream dataStream = httpResponse.GetResponseStream();
                    var streamReader = new StreamReader(dataStream);
                    var responsePlain = streamReader.ReadToEnd();

                    var result = JsonConvert.DeserializeObject<T>(responsePlain);

                    return result;
                }
            }
            catch(Exception ex)
            {
                return default(T);
            }

            return default(T);
        }

        private static WebRequest CreateRequest(string method, List<Tuple<string, string>> parameters)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(Tmdb.SecureBaseUrl);
            stringBuilder.Append(method);

            string parameterSeparator = "?";

            foreach (var parameter in parameters)
            {
                stringBuilder.Append(parameterSeparator);
                stringBuilder.Append(parameter.Item1);
                stringBuilder.Append("=");
                stringBuilder.Append(parameter.Item2);

                parameterSeparator = "&";
            }

            var url = stringBuilder.ToString();
            var request = WebRequest.Create(url);
            return request;
        }
	}
}
