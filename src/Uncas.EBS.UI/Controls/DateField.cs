using System;
using System.Collections.Specialized;
using System.Globalization;
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
        private string _headerResourceName;
        
        public string HeaderResourceName
        {
            get
            {
                return this._headerResourceName;
            }

            set
            {
                this._headerResourceName = value;
                var resourceManager = Resources.Phrases.ResourceManager;
                this.HeaderText = resourceManager.GetString(value);
            }
        }

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

                    // If data field, use datebox:
                    if (!string.IsNullOrEmpty(this.DataField))
                    {
                        control = box;
                    }
                }
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