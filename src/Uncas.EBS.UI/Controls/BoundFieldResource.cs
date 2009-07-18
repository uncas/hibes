using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    public class BoundFieldResource : BoundField
    {
        public string HeaderResourceName
        {
            set
            {
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