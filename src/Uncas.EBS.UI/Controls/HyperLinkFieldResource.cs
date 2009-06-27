using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    public class HyperLinkFieldResource : HyperLinkField
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