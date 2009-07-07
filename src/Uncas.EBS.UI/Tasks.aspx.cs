using System;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI
{
    public partial class Tasks : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            gvTasks.RowUpdated += new GridViewUpdatedEventHandler(gvTasks_RowUpdated);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            gvTasks.EmptyDataText = Resources.Phrases.NoTasks;
        }

        void gvTasks_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            gvIssue.DataBind();
        }
    }
}