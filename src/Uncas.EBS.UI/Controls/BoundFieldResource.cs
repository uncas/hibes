using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    public class BoundFieldResource : BoundField
    {
        public string HeaderResourceName
        {
            set
            {
                var resourceManager = Resources.Phrases.ResourceManager;
                this.HeaderText = resourceManager.GetString(value);
            }
        }
    }
}