using System;
using Foundation;
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
            return UIImage.FromBundle("profileDefault.png");
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

        public static string LocalizedString(string key)
        {
            var path = NSBundle.MainBundle.PathForResource(Settings.LanguageCodeForMenu(), "lproj");
            var localizedBundle = NSBundle.FromPath(path);
            return localizedBundle.LocalizedString(key, key);
        }
    }
}
