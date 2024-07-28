namespace StudentPortal1.Models.Domain
{
    public class State
    {
        public int StateId { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; } // Foreign key
        public Country Country { get; set; } // Navigation property
        public ICollection<City> Cities { get; set; } // Collection navigation property


    }
}
