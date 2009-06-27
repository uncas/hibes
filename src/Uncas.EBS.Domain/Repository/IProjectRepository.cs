using System.Collections.Generic;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.Domain.Repository
{
    public interface IProjectRepository
    {
        IList<Project> GetProjects();

        ProjectEvaluation GetProjectEvaluation
            (int? projectId
            , int? maxPriority
            , int numberOfSimulations
            , int maxNumberOfHistoricalData);
    }
}