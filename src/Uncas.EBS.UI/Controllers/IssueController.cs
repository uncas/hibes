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

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueController"/> class.
        /// </summary>
        public IssueController()
            : this(App.Repositories)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueController"/> class.
        /// </summary>
        /// <param name="repositories">The repositories.</param>
        public IssueController(IRepositoryFactory repositories)
        {
            this.repositories = repositories;
        }

        private readonly IRepositoryFactory repositories;

        private IIssueRepository IssueRepository
        {
            get
            {
                return repositories.IssueRepository;
            }
        }

        #endregion

        /// <summary>
        /// Gets the issue.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        /// <returns>The issue details.</returns>
        public IssueDetails GetIssue(int issueId)
        {
            var issueView = IssueRepository.GetIssueView
                (issueId, Status.Closed);
            return issueView.Issue;
        }

        /// <summary>
        /// Gets the issues.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="status">The status.</param>
        /// <returns>A list of issue details.</returns>
        public IList<IssueDetails> GetIssues(int? projectId, Status status)
        {
            return IssueRepository.GetIssues(projectId, status);
        }

        /// <summary>
        /// Inserts the issue.
        /// </summary>
        /// <param name="refProjectId">The ref project id.</param>
        /// <param name="title">The title.</param>
        /// <param name="status">The status.</param>
        /// <param name="priority">The priority.</param>
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
            IssueRepository.InsertIssue(issue);
        }

        /// <summary>
        /// Updates the issue.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        /// <param name="title">The title.</param>
        /// <param name="status">The status.</param>
        /// <param name="priority">The priority.</param>
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
            IssueRepository.UpdateIssue(issue);
        }

        /// <summary>
        /// Deletes the issue.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        public void DeleteIssue(int issueId)
        {
            IssueRepository.DeleteIssue(issueId);
        }

        /// <summary>
        /// Adds the one to priority.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        /// <returns>True if succesful.</returns>
        public bool AddOneToPriority(int issueId)
        {
            return IssueRepository.AddOneToPriority(issueId);
        }

        /// <summary>
        /// Subtracts the one from priority.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        /// <returns>True if succesful.</returns>
        public bool SubtractOneFromPriority(int issueId)
        {
            return IssueRepository.SubtractOneFromPriority(issueId);
        }

        /// <summary>
        /// Prioritizes all open issues.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <returns>True if succesful.</returns>
        public bool PrioritizeAllOpenIssues(int? projectId)
        {
            return IssueRepository.PrioritizeAllOpenIssues(projectId);
        }
    }
}