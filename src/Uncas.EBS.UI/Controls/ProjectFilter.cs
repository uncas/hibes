﻿using System.Web.UI.WebControls;
using Uncas.EBS.UI.Controllers;

namespace Uncas.EBS.UI.Controls
{
    public class ProjectFilter : DropDownList
    {
        public ProjectFilter()
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

        public int? ProjectId
        {
            get
            {
                int? projectId = null;
                if (!string.IsNullOrEmpty(this.SelectedValue))
                {
                    projectId = int.Parse(this.SelectedValue);
                }
                return projectId;
            }
        }
    }
}