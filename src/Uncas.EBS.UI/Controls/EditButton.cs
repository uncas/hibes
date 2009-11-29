using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// Represents a button for editing.
    /// </summary>
    public class EditButton : LinkButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditButton"/> class.
        /// </summary>
        public EditButton()
        {
            this.CommandName = "Edit";

            this.Text = Resources.Phrases.Edit;
        }
    }
}