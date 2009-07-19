using System.Web.UI.WebControls;
using System.Resources;

namespace Uncas.EBS.UI.Controls
{
    public class HyperLinkFieldResource : HyperLinkField
    {
        ResourceManager _resourceManager
            = Resources.Phrases.ResourceManager;

        public string HeaderResourceName
        {
            set
            {
                this.HeaderText = _resourceManager.GetString(value);
            }
        }

        public string TextResourceName
        {
            set
            {
                this.Text = _resourceManager.GetString(value);
            }
        }
    }
}