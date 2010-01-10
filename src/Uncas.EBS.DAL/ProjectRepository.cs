using System;
using System.Collections.Generic;
using System.Linq;
using Uncas.EBS.Domain.Repository;
using Model = Uncas.EBS.Domain.Model;

namespace Uncas.EBS.DAL
{
    /// <summary>
    /// Handles storage of projects.
    /// </summary>
    public class ProjectRepository : BaseRepository
        , IProjectRepository
    {
        #region IProjectRepository Members

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Inserts the project.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
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
            this.SubmitChanges();
        }

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        public void DeleteProject(int projectId)
        {
            var project = DB.Projects
                .Where(p => p.ProjectId == projectId)
                .SingleOrDefault();
            DB.Projects.DeleteOnSubmit(project);
            this.SubmitChanges();
        }

        /// <summary>
        /// Updates the project.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="projectId">The project id.</param>
        public void UpdateProject(string projectName, int projectId)
        {
            var project = DB.Projects
                .Where(p => p.ProjectId == projectId)
                .SingleOrDefault();
            project.ProjectName = projectName;
            this.SubmitChanges();
        }

        #endregion
    }
}