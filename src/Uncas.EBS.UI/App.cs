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
        #region Private fields

        internal const double StandardNumberOfHoursPerDayDefault
            = 7.5d;

        internal const int NumberOfSimulations = 1000;

        internal const int MaxNumberOfHistoricalTasks = 50;

        /// <summary>
        /// The defailt lower confidence level.
        /// </summary>
        private const double ConfidenceLowDefault = 0.1d;

        /// <summary>
        /// The default medium confidence level.
        /// </summary>
        private const double ConfidenceMediumDefault = 0.5d;

        /// <summary>
        /// The default high confidence level.
        /// </summary>
        private const double ConfidenceHighDefault = 0.9d;

        private const string LanguageCookieKey = "Language";

        private static ConfidenceLevels confidenceLevels
            = new ConfidenceLevels
                (ConfidenceLow
                , ConfidenceMedium
                , ConfidenceHigh);

        #endregion

        #region Properties

        /// <summary>
        /// Gets the confidence low.
        /// </summary>
        /// <value>The confidence low.</value>
        public static double ConfidenceLow
        {
            get
            {
                return GetDoubleFromAppSettings
                    ("ConfidenceLow",
                    ConfidenceLowDefault);
            }
        }

        /// <summary>
        /// Gets the confidence medium.
        /// </summary>
        /// <value>The confidence medium.</value>
        public static double ConfidenceMedium
        {
            get
            {
                return GetDoubleFromAppSettings
                    ("ConfidenceMedium"
                    , ConfidenceMediumDefault);
            }
        }

        /// <summary>
        /// Gets the confidence high.
        /// </summary>
        /// <value>The confidence high.</value>
        public static double ConfidenceHigh
        {
            get
            {
                return GetDoubleFromAppSettings
                    ("ConfidenceHigh"
                    , ConfidenceHighDefault);
            }
        }

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
                    && context.Request.Cookies[LanguageCookieKey] != null)
                {
                    return context.Request
                        .Cookies[LanguageCookieKey].Value;
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
                HttpCookie cookie = new HttpCookie(LanguageCookieKey, value);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        internal static ConfidenceLevels ConfidenceLevels
        {
            get
            {
                return confidenceLevels;
            }
        }

        internal static double StandardNumberOfHoursPerDay
        {
            get
            {
                return GetDoubleFromAppSettings
                    ("NumberOfHoursPerDay"
                    , StandardNumberOfHoursPerDayDefault);
            }
        }

        #endregion

        #region Private methods

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

        #endregion
    }
}