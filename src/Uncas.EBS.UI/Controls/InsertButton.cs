using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    public class InsertButton : LinkButton
    {
        public InsertButton()
        {
            this.CommandName = "Insert";

            this.Text = Resources.Phrases.Insert;
        }
    }
}