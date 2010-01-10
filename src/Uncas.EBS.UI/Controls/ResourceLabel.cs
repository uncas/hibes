﻿using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// A label with a resource string as the text.
    /// </summary>
    public class ResourceLabel : Label
    {
        private string _resourceName;
        
        public string ResourceName
        {
            get
            {
                return this._resourceName;
            }
        
            set
            {
                this._resourceName = value;
                var resourceManager = Resources.Phrases.ResourceManager;
                this.Text = resourceManager.GetString(value);
            }
        }
    }
}