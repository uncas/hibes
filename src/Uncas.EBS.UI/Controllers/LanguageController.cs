using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Model = Uncas.EBS.Domain.Model;

namespace Uncas.EBS.UI.Controllers
{
    /// <summary>
    /// Controller for languages.
    /// </summary>
    public static class LanguageController
    {
        private static IList<Model.Language> languages;
        
        private static IList<Model.Language> Languages
        {
            get
            {
                if (languages == null)
                {
                    languages = new List<Model.Language>();
                    languages.Add
                        (new Model.Language("da-DK", "dansk"));
                    languages.Add
                        (new Model.Language("en-US", "English"));
                }

                return languages;
            }
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <returns>A list of languages.</returns>
        [SuppressMessage("Microsoft.Design"
            , "CA1024:UsePropertiesWhereAppropriate"
            , Justification = "Read from database")]
        public static IList<Model.Language> GetLanguages()
        {
            return Languages;
        }
    }
}