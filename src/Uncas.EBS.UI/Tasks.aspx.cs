using System;

namespace Uncas.EBS.UI
{
    public partial class Tasks : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvTasks.EmptyDataText = Resources.Phrases.NoTasks;
        }
    }
}