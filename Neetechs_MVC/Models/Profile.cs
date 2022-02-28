using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Neetechs_MVC.Models
{
    public class Profile:IdentityUser
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Profession { get; set; }


        [Display(Name = "Full name")]
        public string? FileName { get; set; }
        public byte[]? File { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; }

    }
}
