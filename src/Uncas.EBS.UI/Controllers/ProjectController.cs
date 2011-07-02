using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Caching;
using Uncas.EBS.ApplicationServices;
using Uncas.EBS.Domain;
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
        private IProjectRepository projectRepo
            = App.Repositories.ProjectRepository;

        private ProjectService projectService
            = new ProjectService(App.Repositories);

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>A list of projects.</returns>
        /// TODO: [Obsolete("Use overload with paging instead")]
        [SuppressMessage("Microsoft.Design"
            , "CA1024:UsePropertiesWhereAppropriate"
            , Justification = "Read from database")]
        public IList<Project> GetProjects()
        {
            var result = projectRepo.GetProjects(new Paging());
            return result;
        }

        private TeamEvaluation GetTeamEvaluation(int? projectId
            , int? maxPriority)
        {
            string cacheKey = string.Format
                (CultureInfo.InvariantCulture
                , "TeamEvaluation-{0}-{1}"
                , projectId
                , maxPriority);
            Cache cache = HttpRuntime.Cache;
            TeamEvaluation teamEvaluation
                = (TeamEvaluation)cache[cacheKey];
            if (teamEvaluation == null)
            {
                teamEvaluation = projectService.GetTeamEvaluation
                    (projectId
                    , maxPriority
                    , App.NumberOfSimulations
                    , App.MaxNumberOfHistoricalTasks
                    , App.StandardNumberOfHoursPerDay);
                cache.Add(cacheKey
                    , teamEvaluation
                    , null
                    , DateTime.Now.AddSeconds(30d)
                    , TimeSpan.Zero
                    , CacheItemPriority.Normal
                    , null);
            }

            return teamEvaluation;
        }

        /// <summary>
        /// Gets the project estimate.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="maxPriority">The max priority.</param>
        /// <returns>A list of project evaluations.</returns>
        public IList<ProjectEvaluation> GetProjectEstimate
            (int? projectId, int? maxPriority)
        {
            var result = new List<ProjectEvaluation>();
            result.Add(GetTeamEvaluation
                (projectId, maxPriority).TotalEvaluation);
            return result;
        }

        /// <summary>
        /// Gets the interval probabilities.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="maxPriority">The max priority.</param>
        /// <returns>A list of interval probabilities.</returns>
        public IList<IntervalProbability> GetIntervalProbabilities
            (int? projectId, int? maxPriority)
        {
            var result = GetTeamEvaluation(projectId, maxPriority)
                .TotalEvaluation.Statistics.Probabilities;
            return result;
        }

        /// <summary>
        /// Gets the issue estimates.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="maxPriority">The max priority.</param>
        /// <returns>A list of issue evaluations.</returns>
        public IEnumerable<IssueEvaluation> GetIssueEstimates
            (int? projectId, int? maxPriority)
        {
            var result = GetTeamEvaluation(projectId, maxPriority)
                .TotalEvaluation.GetIssueEvaluations();
            return result;
        }

        /// <summary>
        /// Gets the completion date confidences.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="maxPriority">The max priority.</param>
        /// <returns>A list of completion date confidences.</returns>
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

        /// <summary>
        /// Gets the selected completion date confidences.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="maxPriority">The max priority.</param>
        /// <returns>A list of completion date confidences.</returns>
        public IEnumerable<CompletionDateConfidence>
            GetSelectedCompletionDateConfidences
            (int? projectId, int? maxPriority)
        {
            var teamEvaluation = GetTeamEvaluation(projectId
                , maxPriority);
            var result = teamEvaluation
                .TotalEvaluation
                .GetSelectedCompletionDateConfidences
                (App.ConfidenceLevels);
            return result;
        }

        /// <summary>
        /// Gets the confidence dates per person.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="maxPriority">The max priority.</param>
        /// <returns>A list of completion date confidences.</returns>
        public IEnumerable<PersonConfidenceDates>
            GetConfidenceDatesPerPerson
            (int? projectId, int? maxPriority)
        {
            var teamEvaluation = GetTeamEvaluation(projectId
                , maxPriority);
            var result = teamEvaluation
                .EvaluationsPerPerson
                .Select(epp => epp.GetPersonConfidenceDates
                    (App.ConfidenceLevels));
            return result;
        }

        /// <summary>
        /// Gets the evaluations per person.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="maxPriority">The max priority.</param>
        /// <returns>A list of project evaluations.</returns>
        public IList<ProjectEvaluation> GetEvaluationsPerPerson
            (int? projectId, int? maxPriority)
        {
            var teamEvaluation = GetTeamEvaluation(projectId
                , maxPriority);
            var result = teamEvaluation
                .EvaluationsPerPerson;
            return result;
        }

        /// <summary>
        /// Inserts the project.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        public void InsertProject(string projectName)
        {
            projectRepo.InsertProject(projectName);
        }

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        public void DeleteProject(int projectId)
        {
            projectRepo.DeleteProject(projectId);
        }

        /// <summary>
        /// Updates the project.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="projectId">The project id.</param>
        public void UpdateProject(string projectName
            , int projectId)
        {
            projectRepo.UpdateProject(projectName
                , projectId);
        }
    }
}