using System;
using System.Collections.Generic;
using Foundation;

namespace MoviesApp
{
    public static class Settings
    {
        public delegate void RefreshLanguageDelegate();

        public static List<Language> SupportedLanguages = new List<Language>()
        {
            new Language { Code = NSLocale.PreferredLanguages[0], Name = "Settings.Language.System" },
            new Language { Code = "en-US", Name = "Settings.Language.English" },
            new Language { Code = "pt-BR", Name = "Settings.Language.Portuguese" }
        };

        private static List<string> SupportedMenuLanguageCodes = new List<string>()
        {
            "en",
            "pt"
        };

        //public static List<Language> GetSupportedLanguages()
        //{
        //    SupportedLanguages[0].Name = Resources.LocalizedString("Settings.SystemLanguage");
        //    return SupportedLanguages;
        //}

        public static int LanguageIndex = 0;

        public static string LanguageCode()
        {
            return SupportedLanguages[LanguageIndex].Code;
        }

        public static string LanguageCodeForMenu()
        {
            string menuLanguageCode = LanguageCode().Substring(0, 2);
            if (!SupportedMenuLanguageCodes.Contains(menuLanguageCode))
            {
                menuLanguageCode = "en";
            }
            return menuLanguageCode;
        }
    }

    public class Language
    {
        public string Code;
        public string Name;
    }

    public class Setting
    {
        public SettingId Id;
        public string Name;
    }

    public enum SettingId
    {
        LANGUAGE
    };
}
