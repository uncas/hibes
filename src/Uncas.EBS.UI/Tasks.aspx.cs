using System;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI
{
    public partial class Tasks : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            fvNewTask.ItemInserted += new FormViewInsertedEventHandler(NewTaskForm_ItemInserted);
            gvTasks.RowUpdated += new GridViewUpdatedEventHandler(TasksGridView_RowUpdated);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            gvTasks.EmptyDataText = Resources.Phrases.NoTasks;
        }

        private void NewTaskForm_ItemInserted
            (object sender
            , FormViewInsertedEventArgs e)
        {
            gvIssue.DataBind();
        }

        private void TasksGridView_RowUpdated
            (object sender
            , GridViewUpdatedEventArgs e)
        {
            gvIssue.DataBind();
        }
    }
}