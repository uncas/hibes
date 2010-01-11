using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// A bound field with a resource string as the header text.
    /// </summary>
    public class BoundFieldResource : BoundField
    {
        private string headerResourceName;

        /// <summary>
        /// Gets or sets the name of the header resource.
        /// </summary>
        /// <value>The name of the header resource.</value>
        public string HeaderResourceName
        {
            get
            {
                return this.headerResourceName;
            }

            set
            {
                this.headerResourceName = value;
                var resourceManager
                    = Resources.Phrases.ResourceManager;
                string headerText = resourceManager.GetString(value);
                if (string.IsNullOrEmpty(headerText))
                {
                    headerText = value;
                }

                this.HeaderText = headerText;
            }
        }
    }
}