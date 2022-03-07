using Neetechs.Model;
using System.ComponentModel.DataAnnotations;

namespace Neetechs_MVC.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(5000)]
        public string Name { get; set; }
        public Product? Product { get; set; }
        public Service? Service { get; set; }
        public Post? Post { get; set; }
        public Report? Report { get; set; }
        public Order? Order { get; set; }
        public Category? Category { get; set; }
    }
}
