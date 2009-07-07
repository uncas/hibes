using System;
using System.Web.UI.WebControls;
using Uncas.EBS.UI.Controllers;

namespace Uncas.EBS.UI
{
    public partial class Default : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            gvIssues.RowCommand
                += new GridViewCommandEventHandler(gvIssues_RowCommand);
            odsIssues.Deleted
                += new ObjectDataSourceStatusEventHandler(odsIssues_Deleted);
            lbPrioritizeAllOpenIssues.Click += new EventHandler(lbPrioritizeAllOpenIssues_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblInfo.Text = string.Empty;
            gvIssues.EmptyDataText = Resources.Phrases.NoIssues;
        }

        private IssueController _issueController
            = new IssueController();

        void gvIssues_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "AddOneToPriority":
                    {
                        Func<int, bool> func =
                            (int _issueId)
                                => _issueController.AddOneToPriority(_issueId);
                        ModifyIssue(e, func);
                        break;
                    }
                case "SubtractOneFromPriority":
                    {
                        Func<int, bool> func =
                            (int _issueId)
                                => _issueController.SubtractOneFromPriority(_issueId);
                        ModifyIssue(e, func);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void ModifyIssue(GridViewCommandEventArgs e
            , Func<int, bool> func)
        {
            int issueId = int.Parse(e.CommandArgument.ToString());
            func(issueId);
            gvIssues.DataBind();
        }

        void odsIssues_Deleted(object sender
            , ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                lblInfo.Text = Resources.Phrases.FailedIssueDeletion;
                e.ExceptionHandled = true;
            }
        }

        void lbPrioritizeAllOpenIssues_Click(object sender, EventArgs e)
        {
            _issueController.PrioritizeAllOpenIssues(psProjects.ProjectId);
            gvIssues.DataBind();
        }
    }
}