using System;
using System.Collections.Generic;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.Tests.Fakes
{
    internal class FakeIssueRepository : IIssueRepository
    {
        #region IIssueRepository Members

        public IList<IssueDetails> GetIssues(int? projectId
            , Status status)
        {
            throw new NotImplementedException();
        }

        public IList<IssueView> GetOpenIssuesAndOpenTasks
            (int? projectId
            , int? maxPriority)
        {
            var result = new List<IssueView>();
            var tasks = new List<TaskDetails>();
            tasks.Add(new TaskDetails { RefPersonId = 1 });
            result.Add(new IssueView(new IssueDetails(), tasks));
            return result;
        }

        public IssueView GetIssueView(int issueId, Status taskStatus)
        {
            throw new NotImplementedException();
        }

        public void InsertIssue(Issue issue)
        {
            throw new NotImplementedException();
        }

        public void UpdateIssue(Issue issue)
        {
            throw new NotImplementedException();
        }

        public void DeleteIssue(int issueId)
        {
            throw new NotImplementedException();
        }

        public bool AddOneToPriority(int issueId)
        {
            throw new NotImplementedException();
        }

        public bool SubtractOneFromPriority(int issueId)
        {
            throw new NotImplementedException();
        }

        public bool PrioritizeAllOpenIssues(int? projectId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}