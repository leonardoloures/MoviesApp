using System;
using System.Collections.Generic;
using UIKit;

namespace MoviesApp
{
    public partial class ItemsTableController : UITableViewController
    {
        public List<string> Items { get; set; }
        public int InitialSelectedItem { get; set; }
        public ItemsTableSource.ItemWasSelectedDelegate ItemWasSelected { get; set; }

        private ItemsTableSource ItemsTableSource;

        public ItemsTableController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            this.ItemsTableSource = new ItemsTableSource(this.Items, this.InitialSelectedItem);
            this.ItemsTableSource.ItemWasSelected = this.ItemWasSelected;
                            
            this.TableView.Source = this.ItemsTableSource;
        }

        public void UpdateItems(List<string> items)
        {
            this.Items = items;
            this.ItemsTableSource.Items = this.Items;
            this.TableView.ReloadData();
        }
    }
}