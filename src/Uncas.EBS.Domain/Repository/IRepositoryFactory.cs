namespace Uncas.EBS.Domain.Repository
{
    public interface IRepositoryFactory
    {
        IIssueRepository IssueRepository { get; }
        ITaskRepository TaskRepository { get; }
        IProjectRepository ProjectRepository { get; }
    }
}