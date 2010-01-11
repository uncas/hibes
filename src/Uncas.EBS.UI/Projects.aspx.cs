using System;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI
{
    /// <summary>
    /// Code behind for projects page.
    /// </summary>
    public partial class Projects : BasePage
    {
        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            odsProjects.Deleted += new ObjectDataSourceStatusEventHandler(ProjectsDataSource_Deleted);
        }

        private void ProjectsDataSource_Deleted
            (object sender
            , ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                lblInfo.Text = Resources.Phrases.FailedProjectDeletion;
                e.ExceptionHandled = true;
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            lblInfo.Text = string.Empty;
        }
    }
}