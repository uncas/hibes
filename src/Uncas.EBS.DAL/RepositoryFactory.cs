using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.DAL
{
    /// <summary>
    /// IoC container for repositories.
    /// </summary>
    public class RepositoryFactory : IRepositoryFactory
    {
        #region IRepositoryFactory Members

        /// <summary>
        /// Gets the issue repository.
        /// </summary>
        /// <value>The issue repository.</value>
        public IIssueRepository IssueRepository
        {
            get { return new IssueRepository(); }
        }

        /// <summary>
        /// Gets the person repository.
        /// </summary>
        /// <value>The person repository.</value>
        public IPersonRepository PersonRepository
        {
            get { return new PersonRepository(); }
        }

        /// <summary>
        /// Gets the person off repository.
        /// </summary>
        /// <value>The person off repository.</value>
        public IPersonOffRepository PersonOffRepository
        {
            get { return new PersonOffRepository(); }
        }

        /// <summary>
        /// Gets the project repository.
        /// </summary>
        /// <value>The project repository.</value>
        public IProjectRepository ProjectRepository
        {
            get { return new ProjectRepository(); }
        }

        /// <summary>
        /// Gets the task repository.
        /// </summary>
        /// <value>The task repository.</value>
        public ITaskRepository TaskRepository
        {
            get { return new TaskRepository(); }
        }

        #endregion
    }
}