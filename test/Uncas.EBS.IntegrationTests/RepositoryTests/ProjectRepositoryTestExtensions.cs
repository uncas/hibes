using System.Linq;
using Uncas.EBS.Domain;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.IntegrationTests.RepositoryTests
{
    public static class ProjectRepositoryTestExtensions
    {
        public static Project GetFirstProject(
            this IProjectRepository projectRepository)
        {
            return projectRepository.GetProjects(
                new Paging(1, 1))
                .FirstOrDefault();
        }
    }
}
