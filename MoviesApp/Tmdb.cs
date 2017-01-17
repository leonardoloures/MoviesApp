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

		private const string ParameterApiKey = "api_key";
		private const string ParameterPrimaryReleaseDateGreaterOrEqual = "primary_release_date.gte";
		private const string ParameterPage = "page";
        private const string ParameterSortBy = "sort_by";

        private const string ParameterValueApiKey = "1f54bd990f1cdfb230adb312546d765d";
        private const string ParameterValueSortByPrimaryReleaseDateAsc = "primary_release_date.asc";

		public async static Task<List<Movie>> GetUpcomingMovies(int page)
		{
			var movies = new List<Movie>();

            var tmdbDiscoverMovieResponse = await Tmdb.CallDiscoverMovie(DateTime.Today, page);
            var tmdbGenreMovieListResponse = await Tmdb.CallGenreMovieList();
            movies = tmdbDiscoverMovieResponse.ToMovieList(tmdbGenreMovieListResponse);

			return movies;
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

        private async static Task<T> CallMethod<T>(string method, List<Tuple<string, string>> parameters)
        {
            var request = Tmdb.CreateRequest(method, parameters);
            WebResponse response = await request.GetResponseAsync();
            var httpResponse = (HttpWebResponse)response;

            try
            {
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    Stream dataStream = httpResponse.GetResponseStream();
                    var streamReader = new StreamReader(dataStream);
                    var responsePlain = streamReader.ReadToEnd();

                    var result = JsonConvert.DeserializeObject<T>(responsePlain);

                    return result;
                }
            }
            catch
            {
                return default(T);
            }

            return default(T);
        }

        private async static Task<TmdbDiscoverMovieResponse> CallDiscoverMovie(DateTime releaseDateGreaterOrEqual, int page)
		{
			var parameters = new List<Tuple<string, string>>
			{
                new Tuple<string, string>(Tmdb.ParameterApiKey, Tmdb.ParameterValueApiKey),
				new Tuple<string, string>(Tmdb.ParameterPrimaryReleaseDateGreaterOrEqual, DateTime.Today.ToString("yyyy-MM-dd")),
				new Tuple<string, string>(Tmdb.ParameterPage, page.ToString()),
                new Tuple<string, string>(Tmdb.ParameterSortBy, Tmdb.ParameterValueSortByPrimaryReleaseDateAsc)
			};

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
	}
}
