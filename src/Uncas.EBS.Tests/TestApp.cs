using Uncas.EBS.DAL;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.Tests
{
    class TestApp
    {
        internal static IRepositoryFactory Repositories = new RepositoryFactory();
    }
}