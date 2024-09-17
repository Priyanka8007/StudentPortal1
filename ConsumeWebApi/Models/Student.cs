using System.ComponentModel.DataAnnotations;

namespace ConsumeWebApi.Models
{
    public class Student
    {
        [Key]
        public Guid StdId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
