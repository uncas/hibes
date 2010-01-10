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
        private static IList<Model.Language> _languages;
        
        private static IList<Model.Language> Languages
        {
            get
            {
                if (_languages == null)
                {
                    _languages = new List<Model.Language>();
                    _languages.Add
                        (new Model.Language("da-DK", "dansk"));
                    _languages.Add
                        (new Model.Language("en-US", "English"));
                }
                return _languages;
            }
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design"
            , "CA1024:UsePropertiesWhereAppropriate"
            , Justification = "Read from database")]
        public static IList<Model.Language> GetLanguages()
        {
            return Languages;
        }
    }
}