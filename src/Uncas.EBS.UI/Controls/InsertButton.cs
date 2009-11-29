using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// Represents a button for inserting.
    /// </summary>
    public class InsertButton : LinkButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsertButton"/> class.
        /// </summary>
        public InsertButton()
        {
            this.CommandName = "Insert";

            this.Text = Resources.Phrases.Insert;
        }
    }
}