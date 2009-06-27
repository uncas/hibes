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

        public ITaskRepository TaskRepository
        {
            get { return new TaskRepository(); }
        }

        public IProjectRepository ProjectRepository
        {
            get { return new ProjectRepository(); }
        }

        #endregion
    }
}