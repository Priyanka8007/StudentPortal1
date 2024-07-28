namespace StudentPortal1.Models.Domain
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public ICollection<State> States { get; set; } // Collection navigation property

    }
}
