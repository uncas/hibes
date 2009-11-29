using System;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.Tests.Fakes
{
    internal class FakeRepositoryFactory : IRepositoryFactory
    {
        #region IRepositoryFactory Members

        public IIssueRepository IssueRepository
        {
            get { return new FakeIssueRepository(); }
        }

        public IPersonRepository PersonRepository
        {
            get { return new FakePersonRepository(); }
        }

        public IPersonOffRepository PersonOffRepository
        {
            get { throw new NotImplementedException(); }
        }

        public IProjectRepository ProjectRepository
        {
            get { throw new NotImplementedException(); }
        }

        public ITaskRepository TaskRepository
        {
            get { return new FakeTaskRepository(); }
        }

        #endregion
    }
}