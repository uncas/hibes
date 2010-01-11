using System.Globalization;
using System.Web.UI.WebControls;
using Uncas.EBS.UI.Controllers;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// Dropdownlist with project selection.
    /// </summary>
    public class ProjectSelection : DropDownList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectSelection"/> class.
        /// </summary>
        public ProjectSelection()
        {
            ProjectController projRepo = new ProjectController();
            foreach (var project in projRepo.GetProjects())
            {
                this.Items.Add
                    (new ListItem
                        (project.ProjectName
                        , project.ProjectId.ToString(CultureInfo.InvariantCulture)));
            }

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
                    projectId
                        = int.Parse
                            (this.SelectedValue
                            , CultureInfo.InvariantCulture);
                }

                return projectId;
            }
        }
    }
}