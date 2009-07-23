using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using Uncas.EBS.ApplicationServices;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.UI.Controllers
{
    /// <summary>
    /// Project controller for the web application.
    /// </summary>
    public class ProjectController
    {
        private const int NumberOfSimulations = 1000;
        private const int MaxNumberOfHistoricalTasks = 50;

        private IProjectRepository _projectRepo
            = App.Repositories.ProjectRepository;

        private ProjectService _projectService
            = new ProjectService(App.Repositories);

        public IList<Project> GetProjects()
        {
            var result = _projectRepo.GetProjects(); ;
            return result;
        }

        private TeamEvaluation GetTeamEvaluation(int? projectId
            , int? maxPriority)
        {
            string cacheKey = string.Format
                ("TeamEvaluation-{0}-{1}"
                , projectId, maxPriority);
            Cache cache = HttpRuntime.Cache;
            TeamEvaluation teamEvaluation
                = (TeamEvaluation)cache[cacheKey];
            if (teamEvaluation == null)
            {
                teamEvaluation = _projectService.GetTeamEvaluation
                    (projectId
                    , maxPriority
                    , NumberOfSimulations
                    , MaxNumberOfHistoricalTasks);
                cache.Add(cacheKey, teamEvaluation, null
                    , DateTime.Now.AddSeconds(30d)
                    , TimeSpan.Zero
                    , CacheItemPriority.Normal
                    , null);
            }
            return teamEvaluation;
        }

        public IList<ProjectEvaluation> GetProjectEstimate
            (int? projectId, int? maxPriority)
        {
            var result = new List<ProjectEvaluation>();
            result.Add(GetTeamEvaluation
                (projectId, maxPriority).TotalEvaluation);
            return result;
        }

        public IList<IntervalProbability> GetIntervalProbabilities
            (int? projectId, int? maxPriority)
        {
            var result = GetTeamEvaluation(projectId, maxPriority)
                .TotalEvaluation.Statistics.Probabilities;
            return result;
        }

        public IEnumerable<IssueEvaluation> GetIssueEstimates
            (int? projectId, int? maxPriority)
        {
            var result = GetTeamEvaluation(projectId, maxPriority)
                .TotalEvaluation.GetIssueEvaluations();
            return result;
        }

        public IEnumerable<CompletionDateConfidence>
            GetCompletionDateConfidences
            (int? projectId, int? maxPriority)
        {
            var teamEvaluation = GetTeamEvaluation(projectId
                , maxPriority);
            var result = teamEvaluation
                .TotalEvaluation.GetCompletionDateConfidences();
            return result;
        }

        public IEnumerable<CompletionDateConfidence>
            GetSelectedCompletionDateConfidences
            (int? projectId, int? maxPriority)
        {
            var teamEvaluation = GetTeamEvaluation(projectId
                , maxPriority);
            var result = teamEvaluation
                .TotalEvaluation
                .GetSelectedCompletionDateConfidences();
            return result;
        }

        public IEnumerable<PersonConfidenceDates>
            GetConfidenceDatesPerPerson
            (int? projectId, int? maxPriority)
        {
            var teamEvaluation = GetTeamEvaluation(projectId
                , maxPriority);
            var result = teamEvaluation
                .EvaluationsPerPerson
                .Select(epp => epp.GetPersonConfidenceDates());
            return result;
        }

        public IList<ProjectEvaluation> GetEvaluationsPerPerson
            (int? projectId, int? maxPriority)
        {
            var teamEvaluation = GetTeamEvaluation(projectId
                , maxPriority);
            var result = teamEvaluation
                .EvaluationsPerPerson;
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

        public void UpdateProject(string projectName
            , int Original_ProjectId)
        {
            _projectRepo.UpdateProject(projectName
                , Original_ProjectId);
        }
    }
}