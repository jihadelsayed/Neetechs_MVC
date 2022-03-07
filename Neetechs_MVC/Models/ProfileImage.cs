using Neetechs.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Neetechs_MVC.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public Profile Profile { get; set; }
        public string? FileName { get; set; }
        public byte[]? File { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; }
    }
}
