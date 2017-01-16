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

		private const string ParameterApiKey = "api_key";
		private const string ParameterPrimaryReleaseDateGreaterOrEqual = "primary_release_date.gte";
		private const string ParameterPage = "page";

		private const string ApiKeyValue = "1f54bd990f1cdfb230adb312546d765d";

		public async static Task<List<Movie>> GetUpcomingMovies(int page)
		{
			var movies = new List<Movie>();

			try
			{
				var request = Tmdb.CreateDiscoverMovieRequest(DateTime.Today, page);
				WebResponse response = await request.GetResponseAsync();
				var httpResponse = (HttpWebResponse)response;

				if (httpResponse.StatusCode == HttpStatusCode.OK)
				{
					Stream dataStream = httpResponse.GetResponseStream();
					var streamReader = new StreamReader(dataStream);
					var responsePlain = streamReader.ReadToEnd();

					var result = JsonConvert.DeserializeObject<TmdbDiscoverMovieResponse>(responsePlain);
					movies = result.ToMovieList();
				}
			}
			catch
			{
			}

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

		private static WebRequest CreateDiscoverMovieRequest(DateTime releaseDateGreaterOrEqual, int page)
		{
			var parameters = new List<Tuple<string, string>>
			{
				new Tuple<string, string>(Tmdb.ParameterApiKey, Tmdb.ApiKeyValue),
				new Tuple<string, string>(Tmdb.ParameterPrimaryReleaseDateGreaterOrEqual, DateTime.Today.ToString("yyyy-MM-dd")),
				new Tuple<string, string>(Tmdb.ParameterPage, page.ToString())
			};

			var request = Tmdb.CreateRequest(Tmdb.MethodDiscoverMovie, parameters);

			return request;
		}
	}
}
