using NUnit.Framework;
using Uncas.EBS.ApplicationServices;

namespace Uncas.EBS.IntegrationTests.ApplicationServicesTests
{
    [TestFixture]
    public class ProjectServiceTests
    {
        [Test]
        public void GetProjectEvaluation_All_PositiveAverage()
        {
            ProjectService projectService
                = new ProjectService(TestApp.Repositories);

            var projectEvaluation
                = projectService.GetProjectEvaluation
                    (null
                    , null
                    , 100
                    , 10);

            Assert.IsNotNull(projectEvaluation.Average);
            Assert.Less(0d, projectEvaluation.Average);
        }
    }
}