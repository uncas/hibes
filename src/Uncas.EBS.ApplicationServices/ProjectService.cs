using System.Linq;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;
using Uncas.EBS.Utility.Simulation;

namespace Uncas.EBS.ApplicationServices
{
    public class ProjectService
    {
        #region Private fields and properties

        private IRepositoryFactory _repositories;

        private IIssueRepository IssueRepository
        {
            get
            {
                return _repositories.IssueRepository;
            }
        }

        private ITaskRepository TaskRepository
        {
            get
            {
                return _repositories.TaskRepository;
            }
        }

        private IPersonRepository PersonRepository
        {
            get
            {
                return _repositories.PersonRepository;
            }
        }

        #endregion

        public ProjectService(IRepositoryFactory repositories)
        {
            _repositories = repositories;
        }

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
            var closedTasks
                = TaskRepository.GetTasks
                (Status.Closed
                , maxNumberOfHistoricalData);
            var personViews
                = PersonRepository.GetPersonViews();

            var simulationEngine = new SimulationEngine(closedTasks);

            // Runs a simulation with all tasks:
            var projectEvaluation
                = simulationEngine.GetProjectEvaluation
                (personViews[0]
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
                    .Select(i =>
                        new IssueView
                        (
                            i.Issue,
                            i.Tasks
                                .Where(t =>
                                    t.RefPersonId
                                    == personView.PersonId)
                                .ToList()
                        )).ToList();
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
    }
}