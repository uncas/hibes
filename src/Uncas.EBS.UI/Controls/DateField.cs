using System;
using System.Collections.Specialized;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal)]
    public class DateField : BoundField
    {
        public string HeaderResourceName
        {
            set
            {
                var resourceManager = Resources.Phrases.ResourceManager;
                this.HeaderText = resourceManager.GetString(value);
            }
        }

        public override void InitializeCell(DataControlFieldCell cell
            , DataControlCellType cellType
            , DataControlRowState rowState
            , int rowIndex)
        {
            // TODO: REFACTOR: Reduce number of statements and calls.

            if (cellType == DataControlCellType.Header)
            {
                base.InitializeCell(cell, cellType, rowState, rowIndex);
            }
            Control control = null;

            if (cellType == DataControlCellType.DataCell)
            {
                if (rowState == DataControlRowState.Normal
                    || rowState == DataControlRowState.Alternate)
                {
                    control = cell;
                }
                else
                {
                    DateBox box = new DateBox();
                    cell.Controls.Add(box);

                    //If we have a data field, bind to the data binding event later
                    if (!string.IsNullOrEmpty(this.DataField))
                        control = box;
                }
            }

            if (control != null && this.Visible)
                control.DataBinding += new EventHandler(control_DataBinding);
        }

        void control_DataBinding(object sender, EventArgs e)
        {
            // TODO: REFACTOR: Reduce number of statements and calls.

            if (sender is TableCell)
            {
                TableCell cell = sender as TableCell;
                object dataItem = DataBinder.GetDataItem
                    (cell.NamingContainer);
                cell.Text = DataBinder.GetPropertyValue
                    (dataItem, this.DataField, "{0:d}");
            }
            else if (sender is DateBox)
            {
                DateBox box = sender as DateBox;

                bool isInsertMode = false;

                // If in insert mode, no text should appear
                if (!isInsertMode)
                {
                    object dataItem = DataBinder.GetDataItem
                        (box.NamingContainer);
                    string datePropertyValue
                        = DataBinder.GetPropertyValue
                        (dataItem, this.DataField, null);
                    if (!string.IsNullOrEmpty(datePropertyValue))
                    {
                        DateTime selectedDate = DateTime.Parse
                            (datePropertyValue);
                        box.SelectedDate = selectedDate;
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        public override void ExtractValuesFromCell
            (IOrderedDictionary dictionary
            , DataControlFieldCell cell
            , DataControlRowState rowState
            , bool includeReadOnly)
        {
            // TODO: REFACTOR: Reduce number of statements and calls.

            base.ExtractValuesFromCell(dictionary
                , cell, rowState, includeReadOnly);
            DateTime? value = null;

            int index = 0;

            if (cell.Controls.Count > 0)
            {
                Control control = cell.Controls[index];
                if (control == null)
                    throw new InvalidOperationException
                        ("The control cannot be extracted");
                DateBox box = ((DateBox)control);
                value = box.SelectedDate;
            }

            if (dictionary.Contains(this.DataField))
                dictionary[this.DataField] = value;
            else
                dictionary.Add(this.DataField, value);
        }
    }
}