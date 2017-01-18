using System;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace MoviesApp
{
    // This class is the internal representation of a cast in MoviesApp
    public class Actor
    {
        public string Character { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string ProfileUrl { get; set; }

        private UIImage ProfileImage;

        public async Task<UIImage> GetProfileImage()
        {
            if (this.ProfileImage == null)
            {
                this.ProfileImage = new UIImage();
                await Task.Run(() =>
                {
                    using (var url = new NSUrl(this.ProfileUrl))
                    using (var data = NSData.FromUrl(url))
                    {
                        if (data == null)
                        {
                            this.ProfileImage = Resources.DefaultProfile();
                        }
                        else
                        {
                            this.ProfileImage = UIImage.LoadFromData(data);
                        }
                    }
                });
            }

            return this.ProfileImage;
        }

    }
}
