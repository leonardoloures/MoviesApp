using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace MoviesApp
{
    public class ItemsTableSource : UITableViewSource
    {
        private const string ItemCellId = "ItemCellId";

        public List<string> Items;
        private int SelectedIndex = 0;

        public delegate void ItemWasSelectedDelegate(int index);
        public ItemWasSelectedDelegate ItemWasSelected { get; set; }

        public ItemsTableSource(List<string> items, int initialSelectedIndex)
        {
            this.Items = items;
            this.SelectedIndex = initialSelectedIndex;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return this.Items.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(ItemsTableSource.ItemCellId);
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, ItemsTableSource.ItemCellId);
            }

            cell.TextLabel.Text = this.Items[indexPath.Row];
            cell.Accessory = indexPath.Row == this.SelectedIndex
                ? UITableViewCellAccessory.Checkmark
                : UITableViewCellAccessory.None;

            return cell;
        }

        public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            this.SelectedIndex = indexPath.Row;
            tableView.ReloadData();

            this.ItemWasSelected(this.SelectedIndex);
        }
    }
}
