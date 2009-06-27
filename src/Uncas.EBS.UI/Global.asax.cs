using System;
using System.Configuration;

namespace Uncas.EBS.UI
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Sets the connection string in the data access layer:
            Uncas.EBS.DAL.Properties.Settings.Default.EBSConnectionString
                = ConfigurationManager
                .ConnectionStrings["hibesConnectionString"].ConnectionString;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}