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
        /// Gets the task repository.
        /// </summary>
        /// <value>The task repository.</value>
        ITaskRepository TaskRepository { get; }

        /// <summary>
        /// Gets the project repository.
        /// </summary>
        /// <value>The project repository.</value>
        IProjectRepository ProjectRepository { get; }
    }
}