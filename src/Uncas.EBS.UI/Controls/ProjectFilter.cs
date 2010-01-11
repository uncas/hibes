using System.Globalization;
using System.Web.UI.WebControls;
using Uncas.EBS.UI.Controllers;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// Dropdownlist with project filter.
    /// </summary>
    public class ProjectFilter : DropDownList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectFilter"/> class.
        /// </summary>
        public ProjectFilter()
        {
            string allProjectsText
                = string.Format
                    (CultureInfo.CurrentCulture
                    , "- {0} -"
                    , Resources.Phrases.AllProjects);
            this.Items.Add(new ListItem(allProjectsText, string.Empty));
            ProjectController projRepo = new ProjectController();
            foreach (var project in projRepo.GetProjects())
            {
                this.Items.Add
                    (new ListItem
                        (project.ProjectName
                        , project.ProjectId.ToString(CultureInfo.InvariantCulture)));
            }

            this.AutoPostBack = true;
            this.DataTextField = "ProjectName";
            this.DataValueField = "ProjectId";
        }

        /// <summary>
        /// Gets the project id.
        /// </summary>
        /// <value>The project id.</value>
        public int? ProjectId
        {
            get
            {
                int? projectId = null;
                if (!string.IsNullOrEmpty(this.SelectedValue))
                {
                    projectId = int.Parse
                        (this.SelectedValue
                        , CultureInfo.InvariantCulture);
                }

                return projectId;
            }
        }
    }
}