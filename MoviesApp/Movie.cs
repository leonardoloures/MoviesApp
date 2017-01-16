using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;

namespace MoviesApp
{
	// This class is the internal representation of a movie in MoviesApp
	public class Movie
	{
		public string Title { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string Overview { get; set; }
		public string PosterUrl { get; set; }

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
							this.PosterImage = UIImage.FromBundle("posterDefault.png");
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
	}
}
