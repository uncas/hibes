using System.Globalization;
using System.Web.UI.WebControls;
using Uncas.EBS.UI.Controllers;

namespace Uncas.EBS.UI.Controls
{
    /// <summary>
    /// Dropdownlist with person selection.
    /// </summary>
    public class PersonSelection : DropDownList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonSelection"/> class.
        /// </summary>
        public PersonSelection()
        {
            AddPersonItems();
        }

        /// <summary>
        /// Gets the person id.
        /// </summary>
        /// <value>The person id.</value>
        public int? PersonId
        {
            get
            {
                int? personId = null;
                if (!string.IsNullOrEmpty(this.SelectedValue))
                {
                    personId = int.Parse
                        (this.SelectedValue
                        , CultureInfo.InvariantCulture);
                }

                return personId;
            }
        }

        private bool showAllOption;

        /// <summary>
        /// Gets or sets a value indicating whether [show all option].
        /// </summary>
        /// <value><c>True</c> if [show all option]; otherwise, <c>false</c>.</value>
        public bool ShowAllOption
        {
            get
            {
                return showAllOption;
            }

            set
            {
                showAllOption = value;
                EnsureChildControls();
                AddPersonItems();
            }
        }

        private void AddPersonItems()
        {
            this.Items.Clear();

            PersonController projRepo = new PersonController();
            if (this.ShowAllOption)
            {
                this.Items.Add
                    (new ListItem
                        (" - " + Resources.Phrases.All + " - "
                        , string.Empty));
            }

            foreach (var person in projRepo.GetPersons())
            {
                this.Items.Add
                    (new ListItem
                        (person.PersonName
                        , person.PersonId.ToString(CultureInfo.InvariantCulture)));
            }
        }
    }
}