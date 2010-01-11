using System;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI
{
    /// <summary>
    /// Code behind for tasks page.
    /// </summary>
    public partial class Tasks : BasePage
    {
        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            fvNewTask.ItemInserted += new FormViewInsertedEventHandler(NewTaskForm_ItemInserted);
            gvTasks.RowUpdated += new GridViewUpdatedEventHandler(TasksGridView_RowUpdated);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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