namespace Uncas.EBS.Domain.Model
{
    /// <summary>
    /// Represents a language.
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        /// <param name="code">The language code.</param>
        /// <param name="name">The name of the language.</param>
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