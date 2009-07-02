using System.Web;
using Uncas.EBS.DAL;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.UI
{
    /// <summary>
    /// Handles common operations for the web application.
    /// </summary>
    internal class App
    {
        /// <summary>
        /// Gets the repositories for the web application.
        /// </summary>
        /// <value>The repositories.</value>
        internal static IRepositoryFactory Repositories
        {
            get
            {
                return new RepositoryFactory();
            }
        }

        private const string _languageCookieKey = "Language";
        /// <summary>
        /// Gets or sets the selected language.
        /// </summary>
        /// <value>The selected language.</value>
        internal string SelectedLanguage
        {
            get
            {
                // Tries to get a cookie with the selected language:
                var context = HttpContext.Current;
                if (context != null
                    && context.Request != null
                    && context.Request.Cookies[_languageCookieKey] != null)
                {
                    return context.Request
                        .Cookies[_languageCookieKey].Value;
                }
                else
                {
                    // If no such cookie:
                    return "da-DK";
                }
            }
            set
            {
                // Sets the cookie value here:
                HttpCookie cookie = new HttpCookie(_languageCookieKey, value);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}