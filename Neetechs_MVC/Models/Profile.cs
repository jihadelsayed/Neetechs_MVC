using Microsoft.AspNetCore.Identity;
using Neetechs.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Neetechs_MVC.Models
{
    public class Profile:IdentityUser
    {
        [Display(Name = "Full name")]
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Profession { get; set; }
        public string? FileName { get; set; }
        public byte[]? File { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; }

        // One to manay relationship
        //public ICollection<Service> Services { get; set; }
        // public ICollection<Post> Posts { get; set; }
        //  public ICollection<Product> Products { get; set; }
        //  public ICollection<Message> Messages { get; set; }
        //public ICollection<Notification> Notifications { get; set; }

    }
}
