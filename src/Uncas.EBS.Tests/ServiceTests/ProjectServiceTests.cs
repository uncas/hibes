using NUnit.Framework;
using Uncas.EBS.ApplicationServices;
using Uncas.EBS.Tests.Fakes;

namespace Uncas.EBS.Tests.ServiceTests
{
    [TestFixture]
    public class ProjectServiceTests
    {
        private readonly ProjectService _service
            = new ProjectService(new FakeRepositoryFactory());

        [Test]
        public void GetTeamEvaluation()
        {
            _service.GetTeamEvaluation
                (null
                , null
                , 100
                , 50
                , 7.5d);
        }
    }
}