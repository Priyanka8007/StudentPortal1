using System.ComponentModel.DataAnnotations;

namespace StudentPortal1.Models.Domain
{
    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; } // Foreign key
        public State State { get; set; } // Navigation property


    } 

}
