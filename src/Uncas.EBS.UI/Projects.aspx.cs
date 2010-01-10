using System;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI
{
    public partial class Projects : BasePage
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {
            lblInfo.Text = string.Empty;
        }
    }
}