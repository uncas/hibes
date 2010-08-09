using System;
using System.Globalization;
using System.Web.UI.WebControls;
using Uncas.EBS.Domain.ViewModel;
using Uncas.EBS.UI.Controllers;
using Uncas.EBS.UI.Helpers;

namespace Uncas.EBS.UI
{
    /// <summary>
    /// Code behind for front page.
    /// </summary>
    public partial class Default : BasePage
    {
        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            gvIssues.RowDataBound
                += new GridViewRowEventHandler(IssuesGridView_RowDataBound);
            gvIssues.RowCommand
                += new GridViewCommandEventHandler(IssuesGridView_RowCommand);
            odsIssues.Deleted
                += new ObjectDataSourceStatusEventHandler(IssuesDataSource_Deleted);
            lbPrioritizeAllOpenIssues.Click
                += new EventHandler(PrioritizeAllOpenIssuesButton_Click);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            lblInfo.Text = string.Empty;
            gvIssues.EmptyDataText = Resources.Phrases.NoIssues;
        }

        private IssueController issueController
            = new IssueController();

        private void IssuesGridView_RowDataBound
            (object sender
            , GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var issueDetails = (IssueDetails)e.Row.DataItem;
                double? fractionElapsed = issueDetails.FractionElapsed;
                StyleHelpers.SetIssueRowStyle(e.Row, fractionElapsed);
            }
        }

        private void IssuesGridView_RowCommand
            (object sender
            , GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "AddOneToPriority":
                    {
                        AddOneToPriority(e);
                        break;
                    }

                case "SubtractOneFromPriority":
                    {
                        SubtractOneFromPriority(e);
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        private void SubtractOneFromPriority(GridViewCommandEventArgs e)
        {
            Func<int, bool> func =
                (int _issueId)
                    => issueController.SubtractOneFromPriority(_issueId);
            ModifyIssue(e, func);
        }

        private void AddOneToPriority(GridViewCommandEventArgs e)
        {
            Func<int, bool> func =
                (int _issueId)
                    => issueController.AddOneToPriority(_issueId);
            ModifyIssue(e, func);
        }

        private void ModifyIssue(GridViewCommandEventArgs e
            , Func<int, bool> func)
        {
            int issueId
                = int.Parse(e.CommandArgument.ToString()
                    , CultureInfo.InvariantCulture);
            func(issueId);
            gvIssues.DataBind();
        }

        private void IssuesDataSource_Deleted
            (object sender
            , ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                lblInfo.Text = Resources.Phrases.FailedIssueDeletion;
                e.ExceptionHandled = true;
            }
        }

        private void PrioritizeAllOpenIssuesButton_Click
            (object sender
            , EventArgs e)
        {
            issueController.PrioritizeAllOpenIssues(pfProjects.ProjectId);
            gvIssues.DataBind();
        }
    }
}