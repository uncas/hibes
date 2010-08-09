using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Tests.ModelTests
{
    [TestFixture]
    public class IssueTests
    {
        [Test]
        public void ConstructIssue_Default_OK()
        {
            Issue issue
                = Issue.ConstructIssue(1, "A", Status.Open, 1);
        }
    }
}
