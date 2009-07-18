namespace Uncas.EBS.Domain.Repository
{
    /// <summary>
    /// Determines the overall storage of issues, tasks and projects.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Gets the issue repository.
        /// </summary>
        /// <value>The issue repository.</value>
        IIssueRepository IssueRepository { get; }

        /// <summary>
        /// Gets the person repository.
        /// </summary>
        /// <value>The person repository.</value>
        IPersonRepository PersonRepository { get; }

        /// <summary>
        /// Gets the person off repository.
        /// </summary>
        /// <value>The person off repository.</value>
        IPersonOffRepository PersonOffRepository { get; }

        /// <summary>
        /// Gets the project repository.
        /// </summary>
        /// <value>The project repository.</value>
        IProjectRepository ProjectRepository { get; }

        /// <summary>
        /// Gets the task repository.
        /// </summary>
        /// <value>The task repository.</value>
        ITaskRepository TaskRepository { get; }
    }
}