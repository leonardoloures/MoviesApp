using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace MoviesApp
{
    public partial class SettingsTableController : UITableViewController
    {
        public Settings.RefreshLanguageDelegate RefreshAppLanguage { get; set; }

        private SettingsTableSource SettingsTableSource;
        
        public SettingsTableController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.SettingsTableSource = new SettingsTableSource(this.NavigationController, this.RefreshAppLanguage);
            this.SettingsTableSource.SettingsList = this.SettingsList();
            this.TableView.Source = this.SettingsTableSource;
        }

        public void RefreshLanguage()
        {
            if (this.SettingsTableSource != null)
            {
                this.SettingsTableSource.SettingsList = this.SettingsList();
                this.TableView.ReloadData();
            }
        }

        private List<Setting> SettingsList()
        {
            var settingsList = new List<Setting>();

            var languageSetting = new Setting()
            {
                Id = SettingId.LANGUAGE,
                Name = Resources.LocalizedString("Settings.Language")
            };

            settingsList.Add(languageSetting);

            return settingsList;
        }
    }
}