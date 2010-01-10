using System.Resources;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// A hyperlink field with a resource string as the header text and/or as the field text.
    /// </summary>
    public class HyperLinkFieldResource : HyperLinkField
    {
        private ResourceManager _resourceManager
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