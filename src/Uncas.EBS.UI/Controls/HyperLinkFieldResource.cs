using System.Resources;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// A hyperlink field with a resource string as the header text and/or as the field text.
    /// </summary>
    public class HyperLinkFieldResource : HyperLinkField
    {
        private ResourceManager resourceManager
            = Resources.Phrases.ResourceManager;

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
                this.HeaderText = resourceManager.GetString(value);
            }
        }

        private string textResourceName;

        /// <summary>
        /// Gets or sets the name of the text resource.
        /// </summary>
        /// <value>The name of the text resource.</value>
        public string TextResourceName
        {
            get
            {
                return this.textResourceName;
            }

            set
            {
                this.textResourceName = value;
                this.Text = resourceManager.GetString(value);
            }
        }
    }
}