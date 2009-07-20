using System.Web.UI;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    [ValidationProperty("Text")]
    [ToolboxData("<{0}:NumberBox runat=server></{0}:NumberBox>")]
    public class NumberBox : CompositeControl, ITextControl
    {
        // TODO: REFACTOR: Reduce number of methods.

        protected TextBox tbNumber;
        protected RegularExpressionValidator revNumber;

        public bool AutoPostBack
        {
            get
            {
                EnsureChildControls();
                return tbNumber.AutoPostBack;
            }
            set
            {
                EnsureChildControls();
                tbNumber.AutoPostBack = value;
            }
        }

        protected override void CreateChildControls()
        {
            tbNumber = new TextBox();
            tbNumber.ID = "tbNumber";

            revNumber = new RegularExpressionValidator();
            revNumber.ControlToValidate = tbNumber.ID;
            revNumber.Text = "?";
            revNumber.ToolTip = "Indtast et gyldigt tal";

            SetValidationExpression();

            this.Controls.Add(tbNumber);

            base.CreateChildControls();
        }

        #region Properties

        private int maxValue = int.MaxValue;
        public int MaxValue
        {
            get
            {
                EnsureChildControls();
                return maxValue;
            }
            set
            {
                EnsureChildControls();
                maxValue = value;
            }
        }

        private int minValue = int.MinValue;
        public int MinValue
        {
            get
            {
                EnsureChildControls();
                return minValue;
            }
            set
            {
                EnsureChildControls();
                minValue = value;
            }
        }

        private int numberOfDigits = 9;
        public int NumberOfDigits
        {
            get
            {
                EnsureChildControls();
                return numberOfDigits;
            }
            set
            {
                EnsureChildControls();
                numberOfDigits = value;
                SetValidationExpression();
            }
        }

        public string Text
        {
            get
            {
                EnsureChildControls();
                if (Number.HasValue)
                    return tbNumber.Text;
                else return string.Empty;
            }
            set
            {
                EnsureChildControls();
                tbNumber.Text = value;
            }
        }

        public int? Number
        {
            // TODO: REFACTOR: Reduce number of statements and calls.
            get
            {
                EnsureChildControls();
                int iOut = 0;
                if (tbNumber.Text == string.Empty || !int.TryParse(tbNumber.Text, out iOut))
                    return null;
                else
                {
                    if (iOut > maxValue) return maxValue;
                    else if (iOut < minValue) return minValue;
                    else return iOut;
                }
            }
            set
            {
                EnsureChildControls();
                if (value.HasValue)
                {
                    if (value.Value > maxValue) tbNumber.Text = maxValue.ToString();
                    else if (value.Value < minValue) tbNumber.Text = minValue.ToString();
                    else tbNumber.Text = value.Value.ToString();
                }
                else tbNumber.Text = string.Empty;
            }
        }

        #endregion

        #region Private methods

        private void SetValidationExpression()
        {
            // Default: "\d{1,9}"
            revNumber.ValidationExpression = @"\d{1," + numberOfDigits + "}";
            SetWidth();
        }

        private void SetWidth()
        {
            tbNumber.Width = new Unit((numberOfDigits + 1) * 7);
        }

        #endregion
    }
}