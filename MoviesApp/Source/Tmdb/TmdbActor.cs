using System;
namespace MoviesApp
{
    // This class represents the TMDb actor returned in queries to the API
    public class TmdbActor
    {
        public string character { get; set; }
        public string name { get; set; }
        public int order { get; set; }
        public string profile_path { get; set; }

        public Actor ToActor()
        {
            return new Actor()
            {
                Character = character,
                Name = name,
                Order = order,
                ProfileUrl = this.GetProfileUrl()
            };
        }

        private string GetProfileUrl()
        {
            // TODO: get these values dynamically
            const string secure_base_url = "https://image.tmdb.org/t/p/";
            const string poster_size = "w185";

            return secure_base_url + poster_size + this.profile_path;

        }
    }
}
