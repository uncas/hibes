using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    public class StatusOptions : RadioButtonList
    {
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