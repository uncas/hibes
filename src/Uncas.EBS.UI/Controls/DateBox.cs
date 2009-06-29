using System;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    public class DateBox : TextBox
    {
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