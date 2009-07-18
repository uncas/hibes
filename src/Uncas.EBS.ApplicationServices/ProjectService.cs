using System.Collections.Generic;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.Simulation;
using Uncas.EBS.Domain.ViewModel;

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

        private IPersonOffRepository PersonOffRepository
        {
            get
            {
                return _repositories.PersonOffRepository;
            }
        }

        #endregion

        public ProjectService(IRepositoryFactory repositories)
        {
            _repositories = repositories;
        }

        public ProjectEvaluation GetProjectEvaluation
            (int? projectId
            , int? maxPriority
            , int numberOfSimulations
            , int maxNumberOfHistoricalData)
        {
            // Fetches data for the simulation:
            var openIssuesAndOpenTasks
                = IssueRepository.GetOpenIssuesAndOpenTasks
                (projectId, maxPriority);
            var closedTasks
                = TaskRepository.GetTasks
                (Status.Closed
                , maxNumberOfHistoricalData);
            var personViews
                = PersonRepository.GetPersonViews();

            // Runs the simulation:
            var simulationEngine = new SimulationEngine(closedTasks);
            var projectEvaluation
                = simulationEngine.GetProjectEvaluation
                (personViews
                , openIssuesAndOpenTasks
                , numberOfSimulations);

            return projectEvaluation;
        }
    }
}