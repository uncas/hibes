using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    public class EditButton : LinkButton
    {
        public EditButton()
        {
            this.CommandName = "Edit";

            this.Text = Resources.Phrases.Edit;
        }
    }
}