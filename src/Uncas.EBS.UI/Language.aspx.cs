using System;

namespace Uncas.EBS.UI
{
    /// <summary>
    /// Code behind for language page.
    /// </summary>
    public partial class Language : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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