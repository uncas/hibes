using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    public class CancelButton : LinkButton
    {
        public CancelButton()
        {
            this.CommandName = "Cancel";

            this.Text = Resources.Phrases.Cancel;
        }
    }
}