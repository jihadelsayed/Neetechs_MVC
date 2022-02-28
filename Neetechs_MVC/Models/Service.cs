using System.ComponentModel.DataAnnotations.Schema;

namespace Neetechs_MVC.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? UserId { get; set; }
        public DateTime? AddDate = new DateTime();

        public DateTime Date { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        public string? FileName { get; set; }
        public byte[]? File { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; }

    }
}
