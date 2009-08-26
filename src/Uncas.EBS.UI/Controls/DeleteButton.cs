using System.Globalization;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    public class DeleteButton : LinkButton
    {
        public DeleteButton()
        {
            this.OnClientClick = string.Format
                (CultureInfo.InvariantCulture
                , "return confirm('{0}');"
                , Resources.Phrases.ConfirmDelete);

            this.CommandName = "Delete";

            this.Text = Resources.Phrases.Delete;
        }
    }
}
