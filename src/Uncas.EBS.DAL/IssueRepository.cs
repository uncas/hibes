using System;
using System.Collections.Generic;
using System.Linq;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;
using Model = Uncas.EBS.Domain.Model;

namespace Uncas.EBS.DAL
{
    public class IssueRepository : BaseRepository, IIssueRepository
    {
        TaskRepository _taskRepo = new TaskRepository();

        #region IIssueRepository Members

        public IList<IssueDetails> GetIssues(int? projectId
            , Model.Status status)
        {
            var dbIssues = db.Issues.Select(i => i);

            // Filters on status:
            if (status != Model.Status.Any)
            {
                dbIssues = dbIssues
                    .Where(i => i.RefStatusId == (int)status);
            }

            // Filters on project:
            if (projectId.HasValue)
            {
                dbIssues = dbIssues
                    .Where(i => i.RefProjectId == projectId.Value);
            }

            // Ordering:
            dbIssues = dbIssues.OrderByDescending(i => i.CreatedDate)
                .OrderBy(i => i.Priority);

            return dbIssues
                .Select(i => GetIssueDetailsFromDbIssue(i))
                .ToList();
        }

        public IssueView GetIssueView(int issueId
            , Model.Status taskStatus)
        {
            TaskRepository taskRepo = new TaskRepository();
            return new IssueView
            {
                Issue = GetIssueDetails(issueId),
                Tasks = taskRepo.GetTasks(issueId, taskStatus)
                    .ToList()
            };
        }

        /// <summary>
        /// Inserts the issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <exception cref="RepositoryException"></exception>
        public void InsertIssue(Model.Issue issue)
        {
            if (string.IsNullOrEmpty(issue.Title))
            {
                throw new RepositoryException("Issue must have a title.");
            }
            // Saves the changes:
            try
            {
                Issue dbIssue = GetDbIssueFromModelIssue(issue);
                db.Issues.InsertOnSubmit(dbIssue);
                base.SubmitChanges();
                // Reads auto-generated id from the database:
                issue.IssueId = dbIssue.IssueId;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public void UpdateIssue(Model.Issue issue)
        {
            if (!issue.IssueId.HasValue)
            {
                throw new RepositoryException("Invalid issue");
            }
            var dbIssue = db.Issues
                .Where(i => i.IssueId == issue.IssueId.Value)
                .SingleOrDefault();
            dbIssue.Priority = issue.Priority;
            dbIssue.RefStatusId = (int)issue.Status;
            dbIssue.Title = issue.Title;
            base.SubmitChanges();
        }

        public void DeleteIssue(int issueId)
        {
            Issue issue = ReadIssue(issueId
                , "Found no such issue to delete.");
            if (db.Tasks.Where(t => t.RefIssueId == issueId).Count() > 0)
            {
                throw new RepositoryException("Issue cannot be deleted when there are still tasks attached.");
            }
            db.Issues.DeleteOnSubmit(issue);
            try
            {
                base.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        private Issue ReadIssue(int issueId, string message)
        {
            Issue issue = db.Issues
                .Where(i => i.IssueId == issueId)
                .SingleOrDefault();
            if (issue == null)
            {
                throw new RepositoryException(message);
            }
            return issue;
        }

        public bool AddOneToPriority(int issueId)
        {
            ChangePriority(issueId, 1);

            return true;
        }

        private void ChangePriority(int issueId, int priorityChange)
        {
            Issue issue = ReadIssue(issueId
                , "Found no such issue to change priority.");
            issue.Priority += priorityChange;
            base.SubmitChanges();
        }

        public bool SubtractOneFromPriority(int issueId)
        {
            ChangePriority(issueId, -1);
            return true;
        }

        public bool PrioritizeAllOpenIssues(int? projectId)
        {
            int priority = 1;
            var issues = db.Issues.Where(i => i.RefStatusId == 1);
            if (projectId.HasValue)
            {
                issues = issues.Where(i => i.RefProjectId == projectId.Value);
            }
            issues = issues
                .OrderBy(i => i.Title)
                .OrderBy(i => i.Priority);
            foreach (Issue issue in issues)
            {
                issue.Priority = priority++;
            }
            base.SubmitChanges();
            return true;
        }

        #endregion

        private Model.Issue GetIssue(int issueId)
        {
            return db.Issues.Where(i => i.IssueId == issueId)
                .Select(i => GetIssueDetailsFromDbIssue(i))
                .SingleOrDefault();
        }

        private IssueDetails GetIssueDetails(int issueId)
        {
            return db.Issues.Where(i => i.IssueId == issueId)
                .Select(i => GetIssueDetailsFromDbIssue(i))
                .SingleOrDefault();
        }

        private IssueDetails GetIssueDetailsFromDbIssue(Issue dbIssue)
        {
            return GetIssueDetailsFromDbIssue(dbIssue
                , dbIssue.Tasks.Count
                , dbIssue.Tasks.Where(t => t.RefStatusId == 1)
                    .Sum(t => t.CurrentEstimateInHours - t.ElapsedHours)
                , dbIssue.Tasks.Sum(t => t.ElapsedHours)
                );
        }

        private IssueDetails GetIssueDetailsFromDbIssue(Issue dbIssue
            , int numberOfTasks
            , decimal? remaining
            , decimal? elapsed)
        {
            double? rem = null;
            if (remaining.HasValue)
            {
                rem = (double)remaining.Value;
            }
            double? el = null;
            if (elapsed.HasValue)
            {
                el = (double)elapsed.Value;
            }
            return new IssueDetails
            {
                IssueId = dbIssue.IssueId,
                CreatedDate = dbIssue.CreatedDate,
                Priority = dbIssue.Priority,
                ProjectName = dbIssue.Project.ProjectName,
                Status = (Model.Status)dbIssue.RefStatusId,
                Title = dbIssue.Title,
                NumberOfTasks = numberOfTasks,
                Remaining = rem,
                Elapsed = el
            };
        }

        private Issue GetDbIssueFromModelIssue(Model.Issue issue)
        {
            ProjectRepository projRepo = new ProjectRepository();
            Project project = projRepo.GetProjectByName(issue.ProjectName);
            int projectId = project.ProjectId;
            var dbIssue = new Issue
                {
                    CreatedDate = issue.CreatedDate,
                    Priority = issue.Priority,
                    RefProjectId = projectId,
                    RefStatusId = (int)issue.Status,
                    Title = issue.Title,
                };
            if (issue.IssueId.HasValue)
                dbIssue.IssueId = issue.IssueId.Value;
            return dbIssue;
        }

        internal IList<IssueView> GetOpenIssuesAndOpenTasks
            (int? projectId, int? maxPriority)
        {
            var issues = db.Issues.Where(i => i.RefStatusId == 1)
                .OrderBy(i => i.Priority).AsQueryable<Issue>();
            if (projectId.HasValue)
            {
                issues = issues.Where
                    (i => i.RefProjectId == projectId.Value);
            }
            if (maxPriority.HasValue)
            {
                issues = issues.Where
                    (i => i.Priority <= maxPriority.Value);
            }

            var result = issues.Select
                (issue => new IssueView
                {
                    Issue = GetIssueDetailsFromDbIssue(issue),
                    Tasks = issue.Tasks
                        .Where(t => t.RefStatusId == 1)
                        .Select(t => _taskRepo.GetModelTaskFromDbTask(t))
                        .ToList()
                });

            return result.ToList();
        }
    }
}