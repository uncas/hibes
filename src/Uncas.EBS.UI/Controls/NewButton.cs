using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    public class NewButton : LinkButton
    {
        public NewButton()
        {
            this.CommandName = "New";

            this.Text = Resources.Phrases.New;
        }
    }
}