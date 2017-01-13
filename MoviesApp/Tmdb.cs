using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace MoviesApp
{
	// This class is responsible for querying TMDb API
	public static class Tmdb
	{
		public static List<Movie> GetUpcomingMovies()
		{
			var movies = new List<Movie>();

			WebRequest request = WebRequest.Create("https://api.themoviedb.org/3/discover/movie?primary_release_date.gte=2017-01-13&api_key=1f54bd990f1cdfb230adb312546d765d");
			WebResponse response = request.GetResponse();
			HttpWebResponse httpResponse = (HttpWebResponse)response;

			if (httpResponse.StatusCode == HttpStatusCode.OK)
			{
				Stream dataStream = httpResponse.GetResponseStream();
				StreamReader streamReader = new StreamReader(dataStream);
				string responsePlain = streamReader.ReadToEnd();

				TmdbDiscoverMovieResponse result = JsonConvert.DeserializeObject<TmdbDiscoverMovieResponse>(responsePlain);
				movies = result.ToMovieList();
			}

			return movies;
		}
	}
}
