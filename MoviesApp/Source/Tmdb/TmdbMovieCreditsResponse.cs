using System;
using System.Collections.Generic;

namespace MoviesApp
{
    // This class represents the TMDb response to movie/{movie_id}/credits method in the API
    public class TmdbMovieCreditsResponse
    {
        public int id { get; set; }
        public IList<TmdbActor> cast { get; set; }

        public List<Actor> ToActorList()
        {
            var actorList = new List<Actor>();

            foreach (var tmdbActor in this.cast)
            {
                actorList.Add(tmdbActor.ToActor());
            }

            return actorList;
        }
    }
}
