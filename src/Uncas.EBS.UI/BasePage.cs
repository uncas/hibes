using System.Threading;
using System.Web.UI;

namespace Uncas.EBS.UI
{
    /// <summary>
    /// Base page for the web pages.
    /// </summary>
    public class BasePage : Page
    {
        /// <summary>
        /// Sets the <see cref="P:System.Web.UI.Page.Culture"/> and <see cref="P:System.Web.UI.Page.UICulture"/> for the current thread of the page.
        /// </summary>
        protected override void InitializeCulture()
        {
            base.InitializeCulture();

            var selectedCulture = new System.Globalization.CultureInfo
                    (App.SelectedLanguage);
            Thread.CurrentThread.CurrentCulture = selectedCulture;
            Thread.CurrentThread.CurrentUICulture = selectedCulture;
        }
    }
}