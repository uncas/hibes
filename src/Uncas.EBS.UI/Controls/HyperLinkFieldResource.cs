using System.Web.UI.WebControls;
using System.Resources;

namespace Uncas.EBS.UI.Controls
{
    public class HyperLinkFieldResource : HyperLinkField
    {
        ResourceManager _resourceManager
            = Resources.Phrases.ResourceManager;

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
                this.HeaderText = _resourceManager.GetString(value);
            }
        }

        private string _textResourceName;
        public string TextResourceName
        {
            get
            {
                return this._textResourceName;
            }
            set
            {
                this._textResourceName = value;
                this.Text = _resourceManager.GetString(value);
            }
        }
    }
}