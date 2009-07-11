using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.ApplicationServices
{
    public class ProjectService
    {
        private IRepositoryFactory _repositories;

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
            return _repositories.ProjectRepository.GetProjectEvaluation
                (projectId
                , maxPriority
                , numberOfSimulations
                , maxNumberOfHistoricalData);
        }
    }
}