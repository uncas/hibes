using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Uncas.EBS.UI.Controllers
{
    public static class LanguageController
    {
        private static IList<Language> _languages;
        private static IList<Language> Languages
        {
            get
            {
                if (_languages == null)
                {
                    _languages = new List<Language>();
                    _languages.Add
                        (new Language("da-DK", "dansk"));
                    _languages.Add
                        (new Language("en-US", "English"));
                }
                return _languages;
            }
        }

        [SuppressMessage("Microsoft.Design"
            , "CA1024:UsePropertiesWhereAppropriate")]
        public static IList<Language> GetLanguages()
        {
            return Languages;
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