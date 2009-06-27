using System.Web.UI.WebControls;
using Uncas.EBS.UI.AppRepository;

namespace Uncas.EBS.UI.Controls
{
    public class ProjectSelection : DropDownList
    {
        public ProjectSelection()
        {
            AppProjectRepository _projRepo = new AppProjectRepository();
            string allProjectsText = string.Format("- {0} -"
                , Resources.Phrases.AllProjects);
            this.Items.Add(new ListItem(allProjectsText, ""));
            foreach (var project in _projRepo.GetProjects())
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
