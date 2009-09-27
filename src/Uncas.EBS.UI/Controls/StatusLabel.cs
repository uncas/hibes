using System.Web.UI.WebControls;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.UI.Controls
{
    public class StatusLabel : Label
    {
        private Status _status;
        public Status Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
                this.Text = value == Status.Open
                    ? Resources.Phrases.Open
                    : Resources.Phrases.Closed;
            }
        }
    }
}