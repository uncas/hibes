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
        #region Constructors with dependency injection

        public IssueController()
            : this(App.Repositories)
        {
        }

        public IssueController(IRepositoryFactory repositories)
        {
            this._repositories = repositories;
        }

        private IRepositoryFactory _repositories;

        private IIssueRepository _issueRepository
        {
            get
            {
                return _repositories.IssueRepository;
            }
        }

        #endregion

        public Issue GetIssue(int issueId)
        {
            var issueView = _issueRepository.GetIssueView(issueId, Status.Closed);
            return issueView.Issue;
        }

        public IList<IssueDetails> GetIssues(int? projectId, Status status)
        {
            return _issueRepository.GetIssues(projectId, status);
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
            _issueRepository.InsertIssue(issue);
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
            _issueRepository.UpdateIssue(issue);
        }

        public void DeleteIssue(int Original_IssueId)
        {
            _issueRepository.DeleteIssue(Original_IssueId);
        }

        public bool AddOneToPriority(int Original_IssueId)
        {
            return _issueRepository.AddOneToPriority(Original_IssueId);
        }

        public bool SubtractOneFromPriority(int Original_IssueId)
        {
            return _issueRepository.SubtractOneFromPriority(Original_IssueId);
        }

        public bool PrioritizeAllOpenIssues(int? projectId)
        {
            return _issueRepository.PrioritizeAllOpenIssues(projectId);
        }
    }
}