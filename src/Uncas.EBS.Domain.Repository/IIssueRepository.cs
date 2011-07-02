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
        /// <returns>A list of issues.</returns>
        IList<IssueDetails> GetIssues(int? projectId, Status status);

        /// <summary>
        /// Gets the open issues and open tasks.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="maxPriority">The max priority.</param>
        /// <returns>A list of issues.</returns>
        IList<IssueView> GetOpenIssuesAndOpenTasks
            (int? projectId, int? maxPriority);

        /// <summary>
        /// Gets the issue view.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        /// <param name="taskStatus">The task status.</param>
        /// <returns>An issue view.</returns>
        IssueView GetIssueView(int issueId, Status taskStatus);

        /// <summary>
        /// Inserts the issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <exception cref="RepositoryException">A repository exception.</exception>
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
        /// <returns>True if succesful.</returns>
        bool AddOneToPriority(int issueId);

        /// <summary>
        /// Subtracts one from the priority.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        /// <returns>True if succesful.</returns>
        bool SubtractOneFromPriority(int issueId);

        /// <summary>
        /// Prioritizes all open issues.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <returns>True if succesful.</returns>
        bool PrioritizeAllOpenIssues(int? projectId);
    }
}