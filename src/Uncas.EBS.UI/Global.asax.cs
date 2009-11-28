using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Uncas.EBS.UI
{
    public class Global : System.Web.HttpApplication
    {
        [SuppressMessage("Microsoft.Security"
            , "CA2109:ReviewVisibleEventHandlers")]
        protected void Application_Start(object sender, EventArgs e)
        {
            // Sets the connection string in the data access layer:
            Uncas.EBS.DAL.Properties.Settings.Default.EBSConnectionString
                = ConfigurationManager
                .ConnectionStrings["hibesConnectionString"].ConnectionString;
        }

        [SuppressMessage("Microsoft.Security"
            , "CA2109:ReviewVisibleEventHandlers")]
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        [SuppressMessage("Microsoft.Security"
            , "CA2109:ReviewVisibleEventHandlers")]
        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        [SuppressMessage("Microsoft.Security"
            , "CA2109:ReviewVisibleEventHandlers")]
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        [SuppressMessage("Microsoft.Security"
            , "CA2109:ReviewVisibleEventHandlers")]
        protected void Application_Error(object sender, EventArgs e)
        {

        }

        [SuppressMessage("Microsoft.Security"
            , "CA2109:ReviewVisibleEventHandlers")]
        protected void Session_End(object sender, EventArgs e)
        {

        }

        [SuppressMessage("Microsoft.Security"
            , "CA2109:ReviewVisibleEventHandlers")]
        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}