using System.Configuration;
using System.Web;
using Uncas.EBS.DAL;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.UI
{
    /// <summary>
    /// Handles common operations for the web application.
    /// </summary>
    public static class App
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
        internal static string SelectedLanguage
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

        internal static ConfidenceLevels ConfidenceLevels
            = new ConfidenceLevels
                (ConfidenceLow
                , ConfidenceMedium
                , ConfidenceHigh);

        public const double ConfidenceLowDefault = 0.1d;

        public static double ConfidenceLow
        {
            get
            {
                return GetDoubleFromAppSettings
                    ("ConfidenceLow",
                    ConfidenceLowDefault);
            }
        }

        public const double ConfidenceMediumDefault = 0.5d;

        public static double ConfidenceMedium
        {
            get
            {
                return GetDoubleFromAppSettings
                    ("ConfidenceMedium"
                    , ConfidenceMediumDefault);
            }
        }

        public const double ConfidenceHighDefault = 0.9d;

        public static double ConfidenceHigh
        {
            get
            {
                return GetDoubleFromAppSettings
                    ("ConfidenceHigh"
                    , ConfidenceHighDefault);
            }
        }



        internal const double StandardNumberOfHoursPerDayDefault
            = 7.5d;
        internal static double StandardNumberOfHoursPerDay
        {
            get
            {
                return GetDoubleFromAppSettings
                    ("NumberOfHoursPerDay"
                    , StandardNumberOfHoursPerDayDefault);
            }
        }

        internal const int NumberOfSimulations = 1000;

        internal const int MaxNumberOfHistoricalTasks = 50;


        private static double GetDoubleFromAppSettings
            (string key
            , double defaultValue)
        {
            string value
                = ConfigurationManager.AppSettings[key];
            double result = 0d;
            if (double.TryParse
                (value
                , out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }
    }
}