namespace StudentPortal1.Models
{
    public class UserActivity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
