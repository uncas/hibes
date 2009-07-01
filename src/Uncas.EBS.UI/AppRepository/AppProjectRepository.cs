using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.Simulation;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.UI.AppRepository
{
    /// <summary>
    /// Project repository - layer for the web application.
    /// </summary>
    public static class AppProjectRepository
    {
        private const int NumberOfSimulations = 1000;
        private const int MaxNumberOfHistoricalTasks = 50;

        private static IProjectRepository _projectRepo
            = App.Repositories.ProjectRepository;

        private static TraceContext Trace = HttpContext.Current.Trace;

        public static IList<Project> GetProjects()
        {
            Trace.Write("GetProjects-Begin");
            var result = _projectRepo.GetProjects(); ;
            Trace.Write("GetProjects-End");
            return result;
        }

        private static ProjectEvaluation GetProjEval(int? projectId, int? maxPriority)
        {
            Trace.Write("GetProjEval-Begin");
            string cacheKey = string.Format("ProjectEvaluation-{0}-{1}"
                , projectId, maxPriority);
            Cache cache = HttpContext.Current.Cache;
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
            Trace.Write("GetProjEval-End");
            return projEval;
        }

        public static IList<ProjectEvaluation> GetProjectEstimate
            (int? projectId, int? maxPriority)
        {
            Trace.Write("GetProjectEstimate-Begin");
            var result = new List<ProjectEvaluation>();
            result.Add(GetProjEval(projectId, maxPriority));
            Trace.Write("GetProjectEstimate-End");
            return result;
        }

        public static IList<IntervalProbability> GetIntervalProbabilities
            (int? projectId, int? maxPriority)
        {
            Trace.Write("GetIntervalProbabilities-Begin");
            var result = GetProjEval(projectId, maxPriority)
                .Statistics.Probabilities;
            Trace.Write("GetIntervalProbabilities-End");
            return result;
        }

        public static IEnumerable<IssueEvaluation> GetIssueEstimates
            (int? projectId, int? maxPriority)
        {
            Trace.Write("GetIssueEstimates-Begin");
            var result = GetProjEval(projectId, maxPriority)
                .GetIssueEvaluations();
            Trace.Write("GetIssueEstimates-End");
            return result;
        }

        public static IEnumerable<CompletionDateConfidence>
            GetCompletionDateConfidences
            (int? projectId, int? maxPriority)
        {
            Trace.Write("GetCompletionDateConfidences-Begin");
            var projectEvaluation = GetProjEval(projectId, maxPriority);
            var result = projectEvaluation.GetCompletionDateConfidences();
            Trace.Write("GetCompletionDateConfidences-End");
            return result;
        }

        public static IEnumerable<CompletionDateConfidence>
            GetSelectedCompletionDateConfidences
            (int? projectId, int? maxPriority)
        {
            Trace.Write("GetSelectedCompletionDateConfidences-Begin");
            var projectEvaluation = GetProjEval(projectId, maxPriority);
            var result = projectEvaluation.GetSelectedCompletionDateConfidences();
            Trace.Write("GetSelectedCompletionDateConfidences-End");
            return result;
        }
    }
}