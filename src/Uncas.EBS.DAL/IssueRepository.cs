﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;
using Model = Uncas.EBS.Domain.Model;

namespace Uncas.EBS.DAL
{
    /// <summary>
    /// Handles storage of issues.
    /// </summary>
    public class IssueRepository
        : BaseRepository
        , IIssueRepository
    {

        #region Public methods (IIssueRepository members)


        /// <summary>
        /// Gets the issues.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public IList<IssueDetails> GetIssues
            (int? projectId
            , Model.Status status)
        {
            var dbIssues = DB.Issues.Select(i => i);

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


        /// <summary>
        /// Gets the issue view.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        /// <param name="taskStatus">The task status.</param>
        /// <returns></returns>
        public IssueView GetIssueView
            (int issueId
            , Model.Status taskStatus)
        {
            TaskRepository taskRepo = new TaskRepository();
            return new IssueView
                (
                    GetIssueDetails(issueId),
                    taskRepo.GetTasks(issueId, taskStatus)
                        .ToList()
                );
        }


        /// <summary>
        /// Gets the open issues and open tasks.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="maxPriority">The max priority.</param>
        /// <returns></returns>
        public IList<IssueView> GetOpenIssuesAndOpenTasks
            (int? projectId
            , int? maxPriority)
        {
            var issues = DB.Issues.Where(i => i.RefStatusId == 1)
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
                (
                    GetIssueDetailsFromDbIssue(issue),
                    issue.Tasks
                        .Where(t => t.RefStatusId == 1)
                        .Select(t => TaskRepository
                            .GetTaskDetailsFromDbTask(t))
                        .ToList()
                ));

            return result.ToList();
        }


        /// <summary>
        /// Inserts the issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <exception cref="RepositoryException"></exception>
        public void InsertIssue
            (Model.Issue issue)
        {
            if (string.IsNullOrEmpty(issue.Title))
            {
                throw new RepositoryException("Issue must have a title.");
            }
            if (issue.RefProjectId <= 0)
            {
                throw new RepositoryException("Project Id must be larger than zero.");
            }
            ProjectRepository projRepo = new ProjectRepository();
            if (!projRepo.GetProjects()
                .Any(p => p.ProjectId == issue.RefProjectId))
            {
                throw new RepositoryException
                    ("Project does not exist.");
            }
            // Saves the changes:
            try
            {
                Issue dbIssue = GetDbIssueFromModelIssue(issue);
                DB.Issues.InsertOnSubmit(dbIssue);
                base.SubmitChanges();
                // Reads auto-generated id from the database:
                issue.IssueId = dbIssue.IssueId;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }


        /// <summary>
        /// Updates the issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        public void UpdateIssue
            (Model.Issue issue)
        {
            if (!issue.IssueId.HasValue)
            {
                throw new RepositoryException("Invalid issue");
            }
            var dbIssue = DB.Issues
                .Where(i => i.IssueId == issue.IssueId.Value)
                .SingleOrDefault();
            dbIssue.Priority = issue.Priority;
            dbIssue.RefStatusId = (int)issue.Status;
            dbIssue.Title = issue.Title;
            base.SubmitChanges();
        }


        /// <summary>
        /// Deletes the issue.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        public void DeleteIssue
            (int issueId)
        {
            Issue issue = ReadIssue(issueId
                , "Found no such issue to delete.");
            if (DB.Tasks.Where(t => t.RefIssueId == issueId).Count() > 0)
            {
                throw new RepositoryException("Issue cannot be deleted when there are still tasks attached.");
            }
            DB.Issues.DeleteOnSubmit(issue);
            try
            {
                base.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }


        /// <summary>
        /// Adds one to the priority.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        /// <returns></returns>
        public bool AddOneToPriority
            (int issueId)
        {
            ChangePriority(issueId, 1);

            return true;
        }


        /// <summary>
        /// Subtracts one from the priority.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        /// <returns></returns>
        public bool SubtractOneFromPriority
            (int issueId)
        {
            ChangePriority(issueId, -1);
            return true;
        }


        /// <summary>
        /// Prioritizes all open issues.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <returns></returns>
        public bool PrioritizeAllOpenIssues
            (int? projectId)
        {
            int priority = 1;
            var issues = DB.Issues.Where(i => i.RefStatusId == 1);
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


        #region Private methods


        private Issue ReadIssue
            (int issueId
            , string message)
        {
            Issue issue = DB.Issues
                .Where(i => i.IssueId == issueId)
                .SingleOrDefault();
            if (issue == null)
            {
                throw new RepositoryException(message);
            }
            return issue;
        }


        private void ChangePriority
            (int issueId
            , int priorityChange)
        {
            Issue issue = ReadIssue(issueId
                , "Found no such issue to change priority.");
            issue.Priority += priorityChange;
            base.SubmitChanges();
        }


        private IssueDetails GetIssueDetails
            (int issueId)
        {
            return DB.Issues
                .Where(i => i.IssueId == issueId)
                .Select(i => GetIssueDetailsFromDbIssue(i))
                .SingleOrDefault();
        }

        [SuppressMessage("Microsoft.Performance"
            , "CA1811:AvoidUncalledPrivateCode"
            , Justification = "Called in Linq. Why not visible to FxCop?")]
        private static IssueDetails GetIssueDetailsFromDbIssue
            (Issue dbIssue)
        {
            int numberOfTasks
                = dbIssue.Tasks.Count;
            decimal? remaining
                = dbIssue.Tasks
                 .Where(t => t.RefStatusId == 1)
                 .Sum(t => t.CurrentEstimateInHours
                     - t.ElapsedHours);
            decimal? elapsed
                = dbIssue.Tasks.Sum(t => t.ElapsedHours);

            return IssueDetails.ReconstructIssueDetails
                (dbIssue.IssueId
                , dbIssue.CreatedDate
                , dbIssue.Priority
                , dbIssue.Project.ProjectName
                , (Model.Status)dbIssue.RefStatusId
                , dbIssue.Title
                , numberOfTasks
                , GetDoubleFromDecimal(remaining)
                , GetDoubleFromDecimal(elapsed)
                );
        }


        private static Issue GetDbIssueFromModelIssue
            (Model.Issue issue)
        {
            var dbIssue = new Issue
                {
                    CreatedDate = issue.CreatedDate,
                    Priority = issue.Priority,
                    RefProjectId = issue.RefProjectId,
                    RefStatusId = (int)issue.Status,
                    Title = issue.Title,
                };
            if (issue.IssueId.HasValue)
                dbIssue.IssueId = issue.IssueId.Value;
            return dbIssue;
        }


        #endregion

    }
}