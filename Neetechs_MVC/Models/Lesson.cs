using System.ComponentModel.DataAnnotations.Schema;

namespace Neetechs_MVC.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? UserId { get; set; }
        public Profile? Profile { get; set; }
        public DateTime? AddDate = DateTime.UtcNow;
        public string Category { get; set; }

        public string Description { get; set; }

        public string? FileName { get; set; }
        public byte[]? File { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; }
    }
}
