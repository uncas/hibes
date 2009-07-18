using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.DAL
{
    public class RepositoryFactory : IRepositoryFactory
    {
        #region IRepositoryFactory Members

        public IIssueRepository IssueRepository
        {
            get { return new IssueRepository(); }
        }

        public IPersonRepository PersonRepository
        {
            get { return new PersonRepository(); }
        }

        public IPersonOffRepository PersonOffRepository
        {
            get { return new PersonOffRepository(); }
        }

        public IProjectRepository ProjectRepository
        {
            get { return new ProjectRepository(); }
        }

        public ITaskRepository TaskRepository
        {
            get { return new TaskRepository(); }
        }

        #endregion
    }
}