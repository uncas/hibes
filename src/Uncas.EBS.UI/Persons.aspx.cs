using System;

namespace Uncas.EBS.UI
{
    /// <summary>
    /// Code behind for persons page.
    /// </summary>
    public partial class Persons : BasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Resources.Phrases.Persons;
        }
    }
}