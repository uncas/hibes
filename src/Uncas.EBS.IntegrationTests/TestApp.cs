using Uncas.EBS.DAL;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.IntegrationTests
{
    class TestApp
    {
        internal static IRepositoryFactory Repositories
            = new RepositoryFactory();
    }
}