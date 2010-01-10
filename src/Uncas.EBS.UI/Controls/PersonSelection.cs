using System.Globalization;
using System.Web.UI.WebControls;
using Uncas.EBS.UI.Controllers;

namespace Uncas.EBS.UI.Controls
{
    public class PersonSelection : DropDownList
    {
        public PersonSelection()
        {
            AddPersonItems();
        }

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

        private bool _showAllOption;

        public bool ShowAllOption
        {
            get
            {
                return _showAllOption;
            }

            set
            {
                _showAllOption = value;
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