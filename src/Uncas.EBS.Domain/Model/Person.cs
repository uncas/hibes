namespace Uncas.EBS.Domain.Model
{
    public class Person
    {
        public Person()
        {
            this.DaysPerWeek = 5;
            this.HoursPerDay = 7.5d;
        }

        //public int PersonId { get; set; }

        //public string PersonName { get; set; }

        public int DaysPerWeek { get; set; }

        public double HoursPerDay { get; set; }
    }
}