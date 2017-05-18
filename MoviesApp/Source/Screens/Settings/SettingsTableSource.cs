using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace MoviesApp
{
    public class SettingsTableSource: UITableViewSource
    {
        public List<Setting> SettingsList;

        private const string SettingCellId = "SettingCellId";

        private UINavigationController NavigationController;
        private Settings.RefreshLanguageDelegate RefreshAppLanguage;

        public SettingsTableSource(UINavigationController navigationController, Settings.RefreshLanguageDelegate refreshAppLanguage)
        {
            this.NavigationController = navigationController;
            this.RefreshAppLanguage = refreshAppLanguage;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return this.SettingsList.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(SettingsTableSource.SettingCellId);
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, SettingsTableSource.SettingCellId);
            }

            cell.TextLabel.Text = this.SettingsList[indexPath.Row].Name;

            return cell;
        }

        public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            if (this.SettingsList[indexPath.Row].Id == SettingId.LANGUAGE)
            {
                var itemsTableController = this.NavigationController.Storyboard.InstantiateViewController("ItemsTableController") as ItemsTableController;
                if (itemsTableController != null)
                {
                    itemsTableController.Items = this.GetLanguagesList();
                    itemsTableController.InitialSelectedItem = Settings.LanguageIndex;
                    itemsTableController.ItemWasSelected = this.LanguageSelected;

                    this.NavigationController.PushViewController(itemsTableController, true);
                }
            }
        }

        public void LanguageSelected(int index)
        {
            Settings.LanguageIndex = index;
            this.RefreshAppLanguage();

            var itemsTableController = this.NavigationController.TopViewController as ItemsTableController;

            itemsTableController.UpdateItems(this.GetLanguagesList());
        }

        private List<string> GetLanguagesList()
        {
            var languagesList = new List<string>();
            foreach (var language in Settings.SupportedLanguages)
            {
                languagesList.Add(string.Format("{0} ({1})",
                                                Resources.LocalizedString(language.Name),
                                                language.Code));
            }

            return languagesList;
        }
    }
}
