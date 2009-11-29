using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// Represents a button for updating.
    /// </summary>
    public class UpdateButton : LinkButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateButton"/> class.
        /// </summary>
        public UpdateButton()
        {
            this.CommandName = "Update";

            this.Text = Resources.Phrases.Update;
        }
    }
}