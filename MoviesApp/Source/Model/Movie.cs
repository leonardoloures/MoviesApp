using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace MoviesApp
{
	// This class is the internal representation of a movie in MoviesApp
	public class Movie
	{
        public int Id { get; set; }
		public string Title { get; set; }
		public DateTime? ReleaseDate { get; set; }
		public string Overview { get; set; }
		public string PosterUrl { get; set; }
        public List<string> Genres { get; set; }
        public decimal Stars { get; set; }
        public string OriginalTitle { get; set; }
        public List<Actor> Cast { get; set; }

		private UIImage PosterImage;

		public async Task<UIImage> GetPosterImage()
		{
			if (this.PosterImage == null)
			{
				this.PosterImage = new UIImage();
				await Task.Run(() =>
				{
					using (var url = new NSUrl(this.PosterUrl))
					using (var data = NSData.FromUrl(url))
					{
						if (data == null)
						{
                            this.PosterImage = Resources.DefaultPoster();
						}
						else
						{
							this.PosterImage = UIImage.LoadFromData(data);
						}
					}
				});
			}

			return this.PosterImage;
		}

        public string GetGenres()
        {
            if (this.Genres.Count == 0)
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();
            var separator = string.Empty;

            foreach (var genre in this.Genres)
            {
                stringBuilder.Append(separator);
                stringBuilder.Append(genre);

                separator = ", ";
            }

            return stringBuilder.ToString();
        }
	}
}
