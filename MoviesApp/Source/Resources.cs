using System;
using UIKit;

namespace MoviesApp
{
    public static class Resources
    {
        public static UIImage DefaultPoster()
        {
            return UIImage.FromBundle("posterDefault.jpg");
        }

        public static UIImage DefaultProfile()
        {
            return UIImage.FromBundle("profileDefault.jpg");
        }

        public static UIImage Calendar()
        {
            return UIImage.FromBundle("calendar.png");
        }

        public static UIImage Search()
        {
            return UIImage.FromBundle("search.png");
        }

        public static UIImage Star()
        {
            return UIImage.FromBundle("star.png");
        }
    }
}
