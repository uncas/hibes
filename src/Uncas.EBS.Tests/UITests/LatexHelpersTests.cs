using NUnit.Framework;
using Uncas.EBS.UI.Helpers;

namespace Uncas.EBS.Tests.UITests
{
    [TestFixture]
    public class LatexHelpersTests
    {
        [Test]
        public void LatexFromCompletionDates()
        {
            LatexHelpers lh = new LatexHelpers();
            string result = lh.LatexFromCompletionDates(null, null);
            Assert.Less(0, result.Length);
        }
    }
}