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
                int? PersonId = null;
                if (!string.IsNullOrEmpty(this.SelectedValue))
                {
                    PersonId = int.Parse(this.SelectedValue);
                }
                return PersonId;
            }
        }

        private bool _showAllOption = false;
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
                        , "")
                    );
            }
            foreach (var Person in projRepo.GetPersons())
            {
                this.Items.Add(new ListItem(Person.PersonName
                    , Person.PersonId.ToString()));
            }
        }
    }
}