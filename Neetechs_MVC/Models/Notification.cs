namespace Neetechs_MVC.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public Boolean Read { get; set; }
        public string? UserId { get; set; }
        public Profile? Profile { get; set; }
    }
}
