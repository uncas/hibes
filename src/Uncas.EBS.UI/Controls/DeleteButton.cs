using System.Globalization;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// Represents a button for deleting something.
    /// </summary>
    public class DeleteButton : LinkButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteButton"/> class.
        /// </summary>
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
