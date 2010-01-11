using System.Web.UI.WebControls;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// A dropdownlist with the status options.
    /// </summary>
    public class StatusSelection : DropDownList
    {
        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
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

        /// <summary>
        /// Binds the specified data source to the control that is derived from the <see cref="T:System.Web.UI.WebControls.ListControl"/> class.
        /// </summary>
        /// <param name="dataSource">An <see cref="T:System.Collections.IEnumerable"/> that represents the data source.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// The cached value of <see cref="P:System.Web.UI.WebControls.ListControl.SelectedIndex"/> is out of range.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// The cached values of <see cref="P:System.Web.UI.WebControls.ListControl.SelectedIndex"/> and <see cref="P:System.Web.UI.WebControls.ListControl.SelectedValue"/> do not match.
        /// </exception>
        protected override void PerformDataBinding(System.Collections.IEnumerable dataSource)
        {
            EnsureChildControls();

            base.PerformDataBinding(dataSource);
        }
    }
}