using System.Threading;
using System.Web.UI;

namespace Uncas.EBS.UI
{
    public class BasePage : Page
    {
        protected override void InitializeCulture()
        {
            base.InitializeCulture();

            App app = new App();
            Thread.CurrentThread.CurrentUICulture
                = new System.Globalization.CultureInfo
                    (app.SelectedLanguage);
        }
    }
}