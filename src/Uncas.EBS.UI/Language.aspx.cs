using System;

namespace Uncas.EBS.UI
{
    public partial class Language : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            App app = new App();
            app.SelectedLanguage = Request.QueryString["Language"];
            Response.Redirect("Default.aspx");
        }
    }
}