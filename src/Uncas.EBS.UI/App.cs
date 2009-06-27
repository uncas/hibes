using System.Web;
using Uncas.EBS.DAL;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.UI
{
    internal class App
    {
        internal static IRepositoryFactory Repositories
        {
            get
            {
                return new RepositoryFactory();
            }
        }

        private const string _languageCookieKey = "Language";
        internal string SelectedLanguage
        {
            get
            {
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
                    return "en";
                }
            }
            set
            {
                HttpCookie cookie = new HttpCookie(_languageCookieKey, value);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}