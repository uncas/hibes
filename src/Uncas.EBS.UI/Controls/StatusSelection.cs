using System.Web.UI.WebControls;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// A dropdownlist with the status options.
    /// </summary>
    public class StatusSelection : DropDownList
    {
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            var openItem = new ListItem
                (Resources.Phrases.Open, Status.Open.ToString());
            openItem.Selected = true;
            this.Items.Add(openItem);
            this.Items.Add(new ListItem
                (Resources.Phrases.Closed, Status.Closed.ToString()));
        }

        protected override void PerformDataBinding(System.Collections.IEnumerable dataSource)
        {
            EnsureChildControls();

            base.PerformDataBinding(dataSource);
        }
    }
}