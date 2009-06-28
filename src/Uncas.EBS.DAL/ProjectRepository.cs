using System;
using System.Collections.Generic;
using System.Linq;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.Simulation;
using Uncas.EBS.Domain.ViewModel;
using Model = Uncas.EBS.Domain.Model;

namespace Uncas.EBS.DAL
{
    public class ProjectRepository : BaseRepository
        , IProjectRepository
    {
        #region IProjectRepository Members

        public IList<Model.Project> GetProjects()
        {
            var result = db.Projects
                // Only shows projects with issues:
                .Where(p => p.Issues.Count > 0)
                .Select
                (p => new Model.Project
                    {
                        ProjectId = p.ProjectId,
                        ProjectName = p.ProjectName
                    })
                .OrderBy(p => p.ProjectName);
            return result.ToList();
        }

        public ProjectEvaluation GetProjectEvaluation
            (int? projectId
            , int? maxPriority
            , int numberOfSimulations
            , int maxNumberOfHistoricalData)
        {
            IssueRepository issueRepo = new IssueRepository();
            TaskRepository taskRepo = new TaskRepository();

            IList<IssueView> issueViews
                = issueRepo.GetOpenIssuesAndOpenTasks
                (projectId, maxPriority);

            var closedTasks
                = db.Tasks
                .Where(t => t.RefStatusId == 2)
                .OrderByDescending(t => t.EndDate)
                .Take(maxNumberOfHistoricalData)
                .Select(t => taskRepo.GetModelTaskFromDbTask(t))
                .ToList();

            Evaluations evals = new Evaluations(closedTasks);
            var projectEvaluation = evals.GetProjectEvaluation
                (issueViews
                , numberOfSimulations);
            return projectEvaluation;
        }

        #endregion

        internal Project GetProjectByName(string projectName)
        {
            Project project = FindProjectByName(projectName);
            if (project == null)
            {
                InsertProject(projectName);
                project = FindProjectByName(projectName);
            }
            return project;
        }

        private Project FindProjectByName(string projectName)
        {
            return db.Projects
                .Where(p => p.ProjectName.Equals(projectName))
                .SingleOrDefault();
        }

        private void InsertProject(string projectName)
        {
            Project project = new Project
            {
                ProjectName = projectName,
                CreatedDate = DateTime.Now
            };
            db.Projects.InsertOnSubmit(project);
            base.SubmitChanges();
        }
    }
}