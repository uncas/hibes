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
            var result = db.Projects
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
            db.Projects.InsertOnSubmit(project);
            base.SubmitChanges();
        }

        public void DeleteProject(int projectId)
        {
            var project = db.Projects
                .Where(p => p.ProjectId == projectId)
                .SingleOrDefault();
            db.Projects.DeleteOnSubmit(project);
            base.SubmitChanges();
        }

        public void UpdateProject(string projectName, int projectId)
        {
            var project = db.Projects
                .Where(p => p.ProjectId == projectId)
                .SingleOrDefault();
            project.ProjectName = projectName;
            base.SubmitChanges();
        }

        #endregion

        #region Internal or private members

        internal Project GetProjectByName(string projectName)
        {
            Project project = FindProjectByName(projectName);
            if (project == null)
            {
                InsertProject(projectName);
                project = FindProjectByName(projectName);
            }
            return project;
        }

        private Project FindProjectByName(string projectName)
        {
            return db.Projects
                .Where(p => p.ProjectName.Equals(projectName))
                .SingleOrDefault();
        }

        #endregion
    }
}