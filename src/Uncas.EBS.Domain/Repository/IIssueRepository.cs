using System.Collections.Generic;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.Domain.Repository
{
    /// <summary>
    /// Handles storage of issues.
    /// </summary>
    public interface IIssueRepository
    {
        /// <summary>
        /// Gets the issues.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        IList<IssueDetails> GetIssues(int? projectId, Status status);

        /// <summary>
        /// Gets the issue view.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        /// <param name="taskStatus">The task status.</param>
        /// <returns></returns>
        IssueView GetIssueView(int issueId, Status taskStatus);

        /// <summary>
        /// Inserts the issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <exception cref="RepositoryException"></exception>
        void InsertIssue(Issue issue);

        /// <summary>
        /// Updates the issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        void UpdateIssue(Issue issue);

        /// <summary>
        /// Deletes the issue.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        void DeleteIssue(int issueId);

        /// <summary>
        /// Adds one to the priority.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        bool AddOneToPriority(int issueId);

        /// <summary>
        /// Subtracts one from the priority.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        bool SubtractOneFromPriority(int issueId);

        bool PrioritizeAllOpenIssues(int? projectId);
    }
}