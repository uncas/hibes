using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    public class DeleteButton : LinkButton
    {
        public DeleteButton()
        {
            this.OnClientClick = string.Format("return confirm('{0}');"
                , Resources.Phrases.ConfirmDelete);

            this.CommandName = "Delete";

            this.Text = Resources.Phrases.Delete;
        }
    }
}
