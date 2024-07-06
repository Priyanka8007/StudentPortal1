using System.ComponentModel.DataAnnotations;

namespace StudentPortal1.Models.Domain
{
    public class Person
    {
        [Key] // This annotation specifies that Id property is the primary key
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CurrentDate { get; set; }

        public Person()
        {
            // Initialize CurrentDate with the current date and time when the object is created
            CurrentDate = DateTime.Now;
        }
    }
}