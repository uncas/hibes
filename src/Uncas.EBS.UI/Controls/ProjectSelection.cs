﻿using System.Web.UI.WebControls;
using Uncas.EBS.UI.Controllers;

namespace Uncas.EBS.UI.Controls
{
    public class ProjectSelection : DropDownList
    {
        public ProjectSelection()
        {
            ProjectController projRepo = new ProjectController();
            foreach (var project in projRepo.GetProjects())
            {
                this.Items.Add(new ListItem(project.ProjectName
                    , project.ProjectId.ToString()));
            }
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