using System.Linq;
using Uncas.EBS.Domain;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;
using Uncas.EBS.Utility.Simulation;

namespace Uncas.EBS.ApplicationServices
{
    /// <summary>
    /// Handles projects.
    /// </summary>
    public class ProjectService
    {
        #region Private fields and properties

        private IRepositoryFactory repositories;

        private IIssueRepository IssueRepository
        {
            get
            {
                return repositories.IssueRepository;
            }
        }

        private ITaskRepository TaskRepository
        {
            get
            {
                return repositories.TaskRepository;
            }
        }

        private IPersonRepository PersonRepository
        {
            get
            {
                return repositories.PersonRepository;
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectService"/> class.
        /// </summary>
        /// <param name="repositories">The repositories.</param>
        public ProjectService(IRepositoryFactory repositories)
        {
            this.repositories = repositories;
        }

        /// <summary>
        /// Gets the team evaluation.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="maxPriority">The max priority.</param>
        /// <param name="numberOfSimulations">The number of simulations.</param>
        /// <param name="maxNumberOfHistoricalData">The max number of historical data.</param>
        /// <param name="standardNumberOfHoursPerDay">The standard number of hours per day.</param>
        /// <returns>The team evaluation.</returns>
        public TeamEvaluation GetTeamEvaluation
            (int? projectId
            , int? maxPriority
            , int numberOfSimulations
            , int maxNumberOfHistoricalData
            , double standardNumberOfHoursPerDay)
        {
            // Fetches all data for the simulations:
            var openIssuesAndOpenTasks
                = IssueRepository.GetOpenIssuesAndOpenTasks
                (projectId, maxPriority);
            var filter = new TaskFilter
            {
                Status = Status.Closed,
                MaxCount = maxNumberOfHistoricalData
            };
            var closedTasks
                = TaskRepository.GetTasks
                (filter);
            var personViews
                = PersonRepository.GetPersonViews(new Paging());
            PersonView personView0 = null;
            if (personViews != null)
            {
                personView0 = personViews.FirstOrDefault();
            }

            var simulationEngine = new SimulationEngine(closedTasks);

            // Runs a simulation with all tasks:
            var projectEvaluation
                = simulationEngine.GetProjectEvaluation
                (personView0
                , openIssuesAndOpenTasks
                , numberOfSimulations
                , standardNumberOfHoursPerDay);

            TeamEvaluation teamEvaluation = new TeamEvaluation
            {
                TotalEvaluation = projectEvaluation
            };

            // Gets a project evaluation for each person individually:
            foreach (PersonView personView in personViews)
            {
                var issuesWithTasksForPerson =
                    openIssuesAndOpenTasks
                    .Select(i => GetIssueViewPerPerson(i, personView))
                    .ToList();
                var evaluationForPerson
                    = simulationEngine.GetProjectEvaluation
                    (personView
                    , issuesWithTasksForPerson
                    , numberOfSimulations
                    , standardNumberOfHoursPerDay);
                teamEvaluation.EvaluationsPerPerson.Add
                    (evaluationForPerson);
            }

            return teamEvaluation;
        }

        private static IssueView GetIssueViewPerPerson
            (IssueView originalIssueView
            , PersonView personView)
        {
            var taskDetails
                = originalIssueView.Tasks
                .Where(t =>
                    t.RefPersonId
                    == personView.PersonId)
                .ToList();
            return new IssueView
                (originalIssueView.Issue
                , taskDetails);
        }
    }
}