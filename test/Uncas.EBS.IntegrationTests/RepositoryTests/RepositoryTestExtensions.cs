using System.Linq;
using Uncas.EBS.Domain;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.IntegrationTests.RepositoryTests
{
    public static class RepositoryTestExtensions
    {
        public static Project GetFirstProject(
            this IProjectRepository projectRepository)
        {
            return projectRepository.GetProjects(
                new Paging(1, 1))
                .FirstOrDefault();
        }

        public static Person GetFirstPerson(
           this IPersonRepository projectRepository)
        {
            return projectRepository.GetPersons(
                new Paging(1, 1))
                .FirstOrDefault();
        }

        public static Person GetPerson(
           this IPersonRepository projectRepository,
           int personId)
        {
            return projectRepository
                .GetPersons(new Paging())
                .SingleOrDefault(p => p.PersonId == personId);
        }
    }
}
