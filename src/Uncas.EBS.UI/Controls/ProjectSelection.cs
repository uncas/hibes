using System.Web.UI.WebControls;
using Uncas.EBS.UI.Controllers;

namespace Uncas.EBS.UI.Controls
{
    public class ProjectSelection : DropDownList
    {
        public ProjectSelection()
        {
            string allProjectsText = string.Format("- {0} -"
                , Resources.Phrases.AllProjects);
            this.Items.Add(new ListItem(allProjectsText, ""));
            ProjectController projRepo = new ProjectController();
            foreach (var project in projRepo.GetProjects())
            {
                this.Items.Add(new ListItem(project.ProjectName
                    , project.ProjectId.ToString()));
            }
            this.AutoPostBack = true;
            this.DataTextField = "ProjectName";
            this.DataValueField = "ProjectId";
        }
    }
}