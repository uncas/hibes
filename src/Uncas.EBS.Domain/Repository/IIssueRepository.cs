using System.Collections.Generic;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.Domain.Repository
{
    public interface IIssueRepository : IRepository
    {
        IList<IssueDetails> GetIssues(int? projectId, Status status);

        IssueView GetIssueView(int issueId, Status taskStatus);

        /// <summary>
        /// Inserts the issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <exception cref="RepositoryException"></exception>
        void InsertIssue(Issue issue);

        void UpdateIssue(Issue issue);

        void DeleteIssue(int issueId);
    }
}