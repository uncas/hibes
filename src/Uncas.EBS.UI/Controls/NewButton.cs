using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// Represents a button for creating something new.
    /// </summary>
    public class NewButton : LinkButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewButton"/> class.
        /// </summary>
        public NewButton()
        {
            this.CommandName = "New";

            this.Text = Resources.Phrases.New;
        }
    }
}