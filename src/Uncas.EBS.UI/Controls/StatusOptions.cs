using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// A radio button list with the status options.
    /// </summary>
    public class StatusOptions : RadioButtonList
    {
        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            if (this.Items.Count == 0)
            {
                var openItem = new ListItem
                    (Resources.Phrases.Open, "1");
                openItem.Selected = true;
                this.Items.Add(openItem);
                this.Items.Add(new ListItem
                    (Resources.Phrases.Closed, "2"));
                this.Items.Add(new ListItem
                    (Resources.Phrases.All, "0"));
            }

            this.AutoPostBack = true;
            this.RepeatDirection = RepeatDirection.Horizontal;
        }
    }
}