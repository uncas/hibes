using System.Collections.Generic;

namespace Uncas.EBS.UI.AppRepository
{
    public class AppLanguageRepository
    {
        public IList<Language> GetLanguages()
        {
            var languages = new List<Language>();
            languages.Add(new Language("da-DK", "dansk"));
            languages.Add(new Language("en-US", "English"));
            return languages;
        }
    }

    public class Language
    {
        public Language(string code, string name)
        {
            this.LanguageCode = code;
            this.LanguageName = name;
        }

        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
    }
}