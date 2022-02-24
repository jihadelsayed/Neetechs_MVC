using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Neetechs_MVC.Models
{
    public class Profile:IdentityUser
    {
        //public int Id { get; set; }
        //public string? UserId { get; set; }

        public string FullName { get; set; }
        public string Description { get; set; }
        public string Profession { get; set; }



        public string? FileName { get; set; }
        public byte[]? File { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; }

    }
}
