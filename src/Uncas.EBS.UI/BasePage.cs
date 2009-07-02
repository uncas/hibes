using System.Threading;
using System.Web.UI;
using System;

namespace Uncas.EBS.UI
{
    /// <summary>
    /// Base page for the web pages.
    /// </summary>
    public class BasePage : Page
    {
        protected override void InitializeCulture()
        {
            base.InitializeCulture();

            App app = new App();
            var selectedCulture = new System.Globalization.CultureInfo
                    (app.SelectedLanguage);
            Thread.CurrentThread.CurrentCulture = selectedCulture;
            Thread.CurrentThread.CurrentUICulture = selectedCulture;
        }
    }
}