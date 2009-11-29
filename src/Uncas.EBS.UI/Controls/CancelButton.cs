using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// Represents a button for cancelling an action.
    /// </summary>
    public class CancelButton : LinkButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelButton"/> class.
        /// </summary>
        public CancelButton()
        {
            this.CommandName = "Cancel";

            this.Text = Resources.Phrases.Cancel;
        }
    }
}