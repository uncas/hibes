using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.Simulation;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.UI.Controllers
{
    /// <summary>
    /// Project repository - layer for the web application.
    /// </summary>
    public class ProjectController
    {
        private const int NumberOfSimulations = 1000;
        private const int MaxNumberOfHistoricalTasks = 50;

        private IProjectRepository _projectRepo
            = App.Repositories.ProjectRepository;

        public IList<Project> GetProjects()
        {
            var result = _projectRepo.GetProjects(); ;
            return result;
        }

        private ProjectEvaluation GetProjEval(int? projectId, int? maxPriority)
        {
            string cacheKey = string.Format("ProjectEvaluation-{0}-{1}"
                , projectId, maxPriority);
            Cache cache = HttpRuntime.Cache;
            ProjectEvaluation projEval = (ProjectEvaluation)cache[cacheKey];
            if (projEval == null)
            {
                projEval = _projectRepo.GetProjectEvaluation
                    (projectId
                    , maxPriority
                    , NumberOfSimulations
                    , MaxNumberOfHistoricalTasks);
                cache.Add(cacheKey, projEval, null
                    , DateTime.Now.AddSeconds(30d)
                    , TimeSpan.Zero
                    , CacheItemPriority.Normal
                    , null);
            }
            return projEval;
        }

        public IList<ProjectEvaluation> GetProjectEstimate
            (int? projectId, int? maxPriority)
        {
            var result = new List<ProjectEvaluation>();
            result.Add(GetProjEval(projectId, maxPriority));
            return result;
        }

        public IList<IntervalProbability> GetIntervalProbabilities
            (int? projectId, int? maxPriority)
        {
            var result = GetProjEval(projectId, maxPriority)
                .Statistics.Probabilities;
            return result;
        }

        public IEnumerable<IssueEvaluation> GetIssueEstimates
            (int? projectId, int? maxPriority)
        {
            var result = GetProjEval(projectId, maxPriority)
                .GetIssueEvaluations();
            return result;
        }

        public IEnumerable<CompletionDateConfidence>
            GetCompletionDateConfidences
            (int? projectId, int? maxPriority)
        {
            var projectEvaluation = GetProjEval(projectId, maxPriority);
            var result = projectEvaluation.GetCompletionDateConfidences();
            return result;
        }

        public IEnumerable<CompletionDateConfidence>
            GetSelectedCompletionDateConfidences
            (int? projectId, int? maxPriority)
        {
            var projectEvaluation = GetProjEval(projectId, maxPriority);
            var result = projectEvaluation.GetSelectedCompletionDateConfidences();
            return result;
        }

        public void InsertProject(string projectName)
        {
            _projectRepo.InsertProject(projectName);
        }

        public void DeleteProject(int Original_ProjectId)
        {
            _projectRepo.DeleteProject(Original_ProjectId);
        }

        public void UpdateProject(string projectName, int Original_ProjectId)
        {
            _projectRepo.UpdateProject(projectName, Original_ProjectId);
        }
    }
}