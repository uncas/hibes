using System.Collections.Generic;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Domain.ViewModel
{
    public class IssueView
    {
        public IssueDetails Issue { get; set; }
        public IList<Task> Tasks { get; set; }
    }
}