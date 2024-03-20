using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace StudentPortal1.Models.Domain
{
    public class Student
    {
        [Key]
        public Guid StdId { get; set; }

        public string Name  { get; set; }

        public int Age { get; set; }


    }
}
