using Foundation;
using System;
using UIKit;

namespace MoviesApp
{
    public partial class TableViewLoadingCell : UITableViewCell
    {
        public TableViewLoadingCell (IntPtr handle) : base (handle)
        {
        }

		public TableViewLoadingCell(UITableViewCellStyle style, string reuseIdentifier) : base(style, reuseIdentifier)
		{
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            this.TitleText.Text = Resources.LocalizedString("MoviesList.More");
        }

		public void StartAnimating()
		{
			this.TitleText.Hidden = true;
			this.ActivityIndicatorView.Hidden = false;
			this.ActivityIndicatorView.StartAnimating();
		}

		public void StopAnimating()
		{
			this.ActivityIndicatorView.StopAnimating();
			this.ActivityIndicatorView.Hidden = true;
			this.TitleText.Hidden = false;
		}
    }
}