using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    [ValidationProperty("Text")]
    [ToolboxData("<{0}:NumberBox runat=server></{0}:NumberBox>")]
    public class NumberBox : CompositeControl, ITextControl
    {
        private TextBox numberTextBox;
        
        private RegularExpressionValidator revNumber;

        public bool AutoPostBack
        {
            get
            {
                EnsureChildControls();
                return numberTextBox.AutoPostBack;
            }
            
            set
            {
                EnsureChildControls();
                numberTextBox.AutoPostBack = value;
            }
        }

        protected override void CreateChildControls()
        {
            numberTextBox = new TextBox();
            numberTextBox.ID = "tbNumber";

            revNumber = new RegularExpressionValidator();
            revNumber.ControlToValidate = numberTextBox.ID;
            revNumber.Text = "?";
            revNumber.ToolTip = "Indtast et gyldigt tal";

            SetValidationExpression();

            this.Controls.Add(numberTextBox);

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
                {
                    return numberTextBox.Text;
                }
                else
                {
                    return string.Empty;
                }
            }

            set
            {
                EnsureChildControls();
                numberTextBox.Text = value;
            }
        }

        public int? Number
        {
            get
            {
                EnsureChildControls();
                int result = 0;
                if (!int.TryParse(numberTextBox.Text, out result))
                {
                    return null;
                }
                else
                {
                    if (result > maxValue)
                    {
                        return maxValue;
                    }
                    else if (result < minValue)
                    {
                        return minValue;
                    }
                    else
                    {
                        return result;
                    }
                }
            }

            set
            {
                EnsureChildControls();
                if (value.HasValue)
                {
                    if (value.Value > maxValue)
                    {
                        numberTextBox.Text
                            = maxValue.ToString
                            (CultureInfo.CurrentCulture);
                    }
                    else if (value.Value < minValue)
                    {
                        numberTextBox.Text
                            = minValue.ToString
                            (CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        numberTextBox.Text
                            = value.Value.ToString
                            (CultureInfo.CurrentCulture);
                    }
                }
                else
                {
                    numberTextBox.Text = string.Empty;
                }
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
            numberTextBox.Width = new Unit((numberOfDigits + 1) * 7);
        }

        #endregion
    }
}