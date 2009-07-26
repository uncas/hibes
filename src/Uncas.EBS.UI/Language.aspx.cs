using System;

namespace Uncas.EBS.UI
{
    public partial class Language : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            App.SelectedLanguage = Request.QueryString["Language"];
            string redirectUrl =
               Request.UrlReferrer != null
               ? Request.UrlReferrer.AbsoluteUri
               : "Default.aspx";
            Response.Redirect(redirectUrl);
        }
    }
}