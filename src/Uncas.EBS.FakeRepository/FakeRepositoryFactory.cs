using System;
using NMock2;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.FakeRepository
{
    public class FakeRepositoryFactory : IRepositoryFactory
    {
        #region IRepositoryFactory Members

        public IIssueRepository IssueRepository
        {
            get
            {
                Mockery mocks = new Mockery();
                var issueRepository = mocks.NewMock<IIssueRepository>();

                Expect.Once.On(issueRepository)
                    .Method("DeleteIssue");

                Expect.Once.On(issueRepository)
                    .Method("AddOneToPriority");

                return issueRepository;
            }
        }

        public IPersonOffRepository PersonOffRepository
        {
            get { throw new NotImplementedException(); }
        }

        public IProjectRepository ProjectRepository
        {
            get
            {
                Mockery mocks = new Mockery();
                
                var projectRepository = mocks.NewMock<IProjectRepository>();

                Expect.Once.On(projectRepository)
                    .Method("GetProjectEvaluation");

                return projectRepository;
            }
        }

        public ITaskRepository TaskRepository
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}