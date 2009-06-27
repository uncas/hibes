using System.Web.UI.WebControls;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.UI.Controls
{
    public class StatusLabel : Label
    {
        public Status Status
        {
            set
            {
                this.Text = value == Status.Open
                    ? Resources.Phrases.Open
                    : Resources.Phrases.Closed;
            }
        }
    }
}