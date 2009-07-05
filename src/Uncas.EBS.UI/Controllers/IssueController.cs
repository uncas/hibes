using System.Collections.Generic;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.UI.Controllers
{
    /// <summary>
    /// Issue repository - layer for the web application.
    /// </summary>
    public class IssueController
    {
        private IIssueRepository _parent
            = App.Repositories.IssueRepository;

        public Issue GetIssue(int issueId)
        {
            var issueView = _parent.GetIssueView(issueId, Status.Closed);
            return issueView.Issue;
        }

        public IList<IssueDetails> GetIssues(int? projectId, Status status)
        {
            return _parent.GetIssues(projectId, status);
        }

        public void InsertIssue(string projectName
            , string title
            , Status status
            , int priority)
        {
            Issue issue = new Issue
            {
                ProjectName = projectName,
                Title = title,
                Status = status,
                Priority = priority
            };
            _parent.InsertIssue(issue);
        }

        public void UpdateIssue(int Original_IssueId
            , string title
            , Status status
            , int priority)
        {
            Issue issue = new Issue
            {
                IssueId = Original_IssueId,
                Title = title,
                Status = status,
                Priority = priority
            };
            _parent.UpdateIssue(issue);
        }

        public void DeleteIssue(int Original_IssueId)
        {
            _parent.DeleteIssue(Original_IssueId);
        }
    }
}