using System;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// Represents a textbox for entering a date.
    /// </summary>
    public class DateBox : TextBox
    {
        /// <summary>
        /// Gets or sets the date object.
        /// </summary>
        /// <value>The date object.</value>
        public object DateObject
        {
            get
            {
                return this.SelectedDate;
            }
            set
            {
                if (!(value is DBNull))
                {
                    this.SelectedDate = (DateTime)value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected date.
        /// </summary>
        /// <value>The selected date.</value>
        public DateTime? SelectedDate
        {
            get
            {
                DateTime selectedDate = DateTime.Now;
                if (DateTime.TryParse(this.Text, out selectedDate))
                {
                    return selectedDate;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value.HasValue)
                {
                    this.Text = value.Value.ToShortDateString();
                }
            }
        }
    }
}