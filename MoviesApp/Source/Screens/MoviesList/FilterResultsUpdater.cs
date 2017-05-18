using System;
using UIKit;

namespace MoviesApp
{
    public class FilterResultsUpdater : UISearchResultsUpdating
    {
        public event Action<string> UpdateSearchResults = delegate { };

        public override void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            this.UpdateSearchResults(searchController.SearchBar.Text);
        }
    }
}
