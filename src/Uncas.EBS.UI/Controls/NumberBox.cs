using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// Represents a number box.
    /// </summary>
    [ValidationProperty("Text")]
    [ToolboxData("<{0}:NumberBox runat=server></{0}:NumberBox>")]
    public class NumberBox : CompositeControl, ITextControl
    {
        private TextBox numberTextBox;

        private RegularExpressionValidator revNumber;

        /// <summary>
        /// Gets or sets a value indicating whether [auto post back].
        /// </summary>
        /// <value><c>True</c> if [auto post back]; otherwise, <c>false</c>.</value>
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

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the max value.
        /// </summary>
        /// <value>The max value.</value>
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

        /// <summary>
        /// Gets or sets the min value.
        /// </summary>
        /// <value>The min value.</value>
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

        /// <summary>
        /// Gets or sets the number of digits.
        /// </summary>
        /// <value>The number of digits.</value>
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

        /// <summary>
        /// Gets or sets the text content of a control.
        /// </summary>
        /// <value>The text content of a control.</value>
        /// <returns>
        /// The text content of a control.
        /// </returns>
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

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
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

                if (result > maxValue)
                {
                    return maxValue;
                }

                if (result < minValue)
                {
                    return minValue;
                }

                return result;
            }

            set
            {
                EnsureChildControls();
                if (!value.HasValue)
                {
                    numberTextBox.Text = string.Empty;
                    return;
                }

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