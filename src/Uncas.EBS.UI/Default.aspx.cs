using System;

namespace Uncas.EBS.UI
{
    public partial class Default : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            odsIssues.Deleted += new System.Web.UI.WebControls.ObjectDataSourceStatusEventHandler(odsIssues_Deleted);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblInfo.Text = string.Empty;
            gvIssues.EmptyDataText = Resources.Phrases.NoIssues;
        }

        void odsIssues_Deleted(object sender, System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                lblInfo.Text = Resources.Phrases.FailedIssueDeletion;
                e.ExceptionHandled = true;
            }
        }
    }
}