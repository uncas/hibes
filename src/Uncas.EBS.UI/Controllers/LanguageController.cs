using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Uncas.EBS.UI.Controllers
{
    /// <summary>
    /// Controller for languages.
    /// </summary>
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

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design"
            , "CA1024:UsePropertiesWhereAppropriate")]
        public static IList<Language> GetLanguages()
        {
            return Languages;
        }
    }

    /// <summary>
    /// Represents a language.
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="name">The name.</param>
        public Language(string code, string name)
        {
            this.LanguageCode = code;
            this.LanguageName = name;
        }

        /// <summary>
        /// Gets or sets the language code.
        /// </summary>
        /// <value>The language code.</value>
        public string LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the language.
        /// </summary>
        /// <value>The name of the language.</value>
        public string LanguageName { get; set; }
    }
}