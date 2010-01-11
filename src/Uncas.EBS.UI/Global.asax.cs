using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Uncas.EBS.UI
{
    /// <summary>
    /// The global application class for the web application.
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Handles the Start event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Security"
            , "CA2109:ReviewVisibleEventHandlers"
            , Justification = "Standard ASP.NET")]
        protected void Application_Start(object sender, EventArgs e)
        {
            // Sets the connection string in the data access layer:
            Uncas.EBS.DAL.Properties.Settings.Default.EBSConnectionString
                = ConfigurationManager
                .ConnectionStrings["hibesConnectionString"].ConnectionString;
        }

        /// <summary>
        /// Handles the Start event of the Session control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Security"
            , "CA2109:ReviewVisibleEventHandlers"
            , Justification = "Standard ASP.NET")]
        protected void Session_Start(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the BeginRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Security"
            , "CA2109:ReviewVisibleEventHandlers"
            , Justification = "Standard ASP.NET")]
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the AuthenticateRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Security"
            , "CA2109:ReviewVisibleEventHandlers"
            , Justification = "Standard ASP.NET")]
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Error event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Security"
            , "CA2109:ReviewVisibleEventHandlers"
            , Justification = "Standard ASP.NET")]
        protected void Application_Error(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the End event of the Session control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Security"
            , "CA2109:ReviewVisibleEventHandlers"
            , Justification = "Standard ASP.NET")]
        protected void Session_End(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the End event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Security"
            , "CA2109:ReviewVisibleEventHandlers"
            , Justification = "Standard ASP.NET")]
        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}