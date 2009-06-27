using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    public class UpdateButton : LinkButton
    {
        public UpdateButton()
        {
            this.CommandName = "Update";

            this.Text = Resources.Phrases.Update;
        }
    }
}