using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// Represents a bound field that handles datetime.
    /// </summary>
    [AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal)]
    public class DateField : BoundField
    {
        private string headerResourceName;

        /// <summary>
        /// Gets or sets the name of the header resource.
        /// </summary>
        /// <value>The name of the header resource.</value>
        public string HeaderResourceName
        {
            get
            {
                return this.headerResourceName;
            }

            set
            {
                this.headerResourceName = value;
                var resourceManager = Resources.Phrases.ResourceManager;
                this.HeaderText = resourceManager.GetString(value);
            }
        }

        /// <summary>
        /// Initializes the specified <see cref="T:System.Web.UI.WebControls.TableCell"/> object to the specified row state.
        /// </summary>
        /// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell"/> to initialize.</param>
        /// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType"/> values.</param>
        /// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState"/> values.</param>
        /// <param name="rowIndex">The zero-based index of the row.</param>
        public override void InitializeCell
            (DataControlFieldCell cell
            , DataControlCellType cellType
            , DataControlRowState rowState
            , int rowIndex)
        {
            if (cellType == DataControlCellType.Header)
            {
                base.InitializeCell(cell
                    , cellType
                    , rowState
                    , rowIndex);
            }

            Control control = GetControl
                (cell
                , cellType
                , rowState);

            if (control != null && this.Visible)
            {
                control.DataBinding
                    += new EventHandler(Control_DataBinding);
            }
        }

        private Control GetControl
            (DataControlFieldCell cell
            , DataControlCellType cellType
            , DataControlRowState rowState)
        {
            if (cellType != DataControlCellType.DataCell)
            {
                return null;
            }

            Control control = null;
            if (rowState == DataControlRowState.Normal
                || rowState == DataControlRowState.Alternate)
            {
                control = cell;
            }
            else
            {
                control = GetControl(cell, control);
            }

            return control;
        }

        private Control GetControl(DataControlFieldCell cell, Control control)
        {
            DateBox box = new DateBox();
            cell.Controls.Add(box);

            // If data field, use datebox:
            if (!string.IsNullOrEmpty(this.DataField))
            {
                control = box;
            }

            return control;
        }

        private void Control_DataBinding
            (object sender
            , EventArgs e)
        {
            TableCell cell = sender as TableCell;
            if (cell != null)
            {
                object dataItem = DataBinder.GetDataItem
                    (cell.NamingContainer);
                cell.Text = DataBinder.GetPropertyValue
                    (dataItem, this.DataField, "{0:d}");
            }
            else
            {
                DateBox box = sender as DateBox;
                if (box != null)
                {
                    object dataItem = DataBinder.GetDataItem
                        (box.NamingContainer);
                    string datePropertyValue
                        = DataBinder.GetPropertyValue
                        (dataItem, this.DataField, null);
                    if (!string.IsNullOrEmpty(datePropertyValue))
                    {
                        DateTime selectedDate
                            = DateTime.Parse
                            (datePropertyValue
                            , CultureInfo.InvariantCulture);
                        box.SelectedDate = selectedDate;
                    }
                }
            }
        }

        /// <summary>
        /// Fills the specified <see cref="T:System.Collections.IDictionary"/> object with the values from the specified <see cref="T:System.Web.UI.WebControls.TableCell"/> object.
        /// </summary>
        /// <param name="dictionary">A <see cref="T:System.Collections.IDictionary"/> used to store the values of the specified cell.</param>
        /// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell"/> that contains the values to retrieve.</param>
        /// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState"/> values.</param>
        /// <param name="includeReadOnly">True to include the values of read-only fields; otherwise, false.</param>
        public override void ExtractValuesFromCell
            (IOrderedDictionary dictionary
            , DataControlFieldCell cell
            , DataControlRowState rowState
            , bool includeReadOnly)
        {
            base.ExtractValuesFromCell
                (dictionary
                , cell
                , rowState
                , includeReadOnly);
            DateTime? value = null;

            int index = 0;

            if (cell.Controls.Count > 0)
            {
                Control control = cell.Controls[index];
                if (control == null)
                {
                    throw new InvalidOperationException
                        ("The control cannot be extracted");
                }

                DateBox box = (DateBox)control;
                value = box.SelectedDate;
            }

            if (dictionary.Contains(this.DataField))
            {
                dictionary[this.DataField] = value;
            }
            else
            {
                dictionary.Add(this.DataField, value);
            }
        }
    }
}