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

        private readonly IRepositoryFactory _repositories;

        private IIssueRepository _issueRepository
        {
            get
            {
                return _repositories.IssueRepository;
            }
        }

        #endregion

        public IssueDetails GetIssue(int issueId)
        {
            var issueView = _issueRepository.GetIssueView
                (issueId, Status.Closed);
            return issueView.Issue;
        }

        public IList<IssueDetails> GetIssues(int? projectId, Status status)
        {
            return _issueRepository.GetIssues(projectId, status);
        }

        public void InsertIssue(int refProjectId
            , string title
            , Status status
            , int priority)
        {
            Issue issue = Issue.ConstructIssue
                (refProjectId
                , title
                , status
                , priority);
            _issueRepository.InsertIssue(issue);
        }

        public void UpdateIssue(int issueId
            , string title
            , Status status
            , int priority)
        {
            Issue issue = Issue.ReconstructIssue
                (issueId
                , title
                , status
                , priority);
            _issueRepository.UpdateIssue(issue);
        }

        public void DeleteIssue(int issueId)
        {
            _issueRepository.DeleteIssue(issueId);
        }

        public bool AddOneToPriority(int issueId)
        {
            return _issueRepository.AddOneToPriority(issueId);
        }

        public bool SubtractOneFromPriority(int issueId)
        {
            return _issueRepository.SubtractOneFromPriority(issueId);
        }

        public bool PrioritizeAllOpenIssues(int? projectId)
        {
            return _issueRepository.PrioritizeAllOpenIssues(projectId);
        }
    }
}