using System.Web.UI.WebControls;
using Uncas.EBS.UI.Controllers;

namespace Uncas.EBS.UI.Controls
{
    public class PersonSelection : DropDownList
    {
        public PersonSelection()
        {
            PersonController projRepo = new PersonController();
            foreach (var Person in projRepo.GetPersons())
            {
                this.Items.Add(new ListItem(Person.PersonName
                    , Person.PersonId.ToString()));
            }
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
    }
}