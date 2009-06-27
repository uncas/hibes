using System.Collections.Generic;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.Simulation;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.UI.AppRepository
{
    /// <summary>
    /// Project repository - layer for the web application.
    /// </summary>
    public class AppProjectRepository
    {
        private const int NumberOfSimulations = 10 * 1000;
        private const int MaxNumberOfHistoricalTasks = 50;

        private IProjectRepository _parent
            = App.Repositories.ProjectRepository;

        public IList<Project> GetProjects()
        {
            return _parent.GetProjects();
        }

        private int? _currentProjectForEstimate = null;
        private int? _currentMaxPriority = null;

        private ProjectEvaluation _currentEvaluation = null;

        private ProjectEvaluation GetProjEval(int? projectId, int? maxPriority)
        {
            if (_currentEvaluation == null
                || projectId != _currentProjectForEstimate
                || maxPriority != _currentMaxPriority)
            {
                _currentEvaluation = _parent.GetProjectEvaluation
                    (projectId
                    , maxPriority
                    , NumberOfSimulations
                    , MaxNumberOfHistoricalTasks);
                _currentMaxPriority = maxPriority;
                _currentProjectForEstimate = projectId;
            }
            return _currentEvaluation;
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
            return GetProjEval(projectId, maxPriority)
                .Statistics.Probabilities;
        }

        public IEnumerable<IssueEvaluation> GetIssueEstimates
            (int? projectId, int? maxPriority)
        {
            return GetProjEval(projectId, maxPriority)
                .GetIssueEvaluations();
        }
    }
}