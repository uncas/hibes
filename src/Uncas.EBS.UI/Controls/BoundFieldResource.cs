using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// A bound field with a resource string as the header text.
    /// </summary>
    public class BoundFieldResource : BoundField
    {
        private string _headerResourceName;
        
        public string HeaderResourceName
        {
            get
            {
                return this._headerResourceName;
            }

            set
            {
                this._headerResourceName = value;
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