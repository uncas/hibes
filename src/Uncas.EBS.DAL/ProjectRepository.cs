using System;
using System.Collections.Generic;
using System.Linq;
using Uncas.EBS.Domain.Repository;
using Model = Uncas.EBS.Domain.Model;

namespace Uncas.EBS.DAL
{
    public class ProjectRepository : BaseRepository
        , IProjectRepository
    {
        #region IProjectRepository Members

        public IList<Model.Project> GetProjects()
        {
            var result = DB.Projects
                .Select
                (p => new Model.Project
                    {
                        ProjectId = p.ProjectId,
                        ProjectName = p.ProjectName
                    })
                .OrderBy(p => p.ProjectName);
            return result.ToList();
        }

        public void InsertProject(string projectName)
        {
            if (string.IsNullOrEmpty(projectName))
            {
                throw new RepositoryException("Project name required");
            }

            Project project = new Project
            {
                ProjectName = projectName,
                CreatedDate = DateTime.Now
            };
            DB.Projects.InsertOnSubmit(project);
            base.SubmitChanges();
        }

        public void DeleteProject(int projectId)
        {
            var project = DB.Projects
                .Where(p => p.ProjectId == projectId)
                .SingleOrDefault();
            DB.Projects.DeleteOnSubmit(project);
            base.SubmitChanges();
        }

        public void UpdateProject(string projectName, int projectId)
        {
            var project = DB.Projects
                .Where(p => p.ProjectId == projectId)
                .SingleOrDefault();
            project.ProjectName = projectName;
            base.SubmitChanges();
        }

        #endregion
    }
}