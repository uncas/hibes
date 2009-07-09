using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    public class ResourceLabel : Label
    {
        public string ResourceName
        {
            set
            {
                var resourceManager = Resources.Phrases.ResourceManager;
                this.Text = resourceManager.GetString(value);
            }
        }
    }
}