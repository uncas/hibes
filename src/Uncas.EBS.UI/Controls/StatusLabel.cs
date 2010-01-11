using System.Web.UI.WebControls;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// A label for the status.
    /// </summary>
    public class StatusLabel : Label
    {
        private Status status;
        
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public Status Status
        {
            get
            {
                return this.status;
            }
            
            set
            {
                this.status = value;
                this.Text = value == Status.Open
                    ? Resources.Phrases.Open
                    : Resources.Phrases.Closed;
            }
        }
    }
}