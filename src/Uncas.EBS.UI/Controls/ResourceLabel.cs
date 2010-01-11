using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// A label with a resource string as the text.
    /// </summary>
    public class ResourceLabel : Label
    {
        private string resourceName;

        /// <summary>
        /// Gets or sets the name of the resource.
        /// </summary>
        /// <value>The name of the resource.</value>
        public string ResourceName
        {
            get
            {
                return this.resourceName;
            }
        
            set
            {
                this.resourceName = value;
                var resourceManager = Resources.Phrases.ResourceManager;
                this.Text = resourceManager.GetString(value);
            }
        }
    }
}