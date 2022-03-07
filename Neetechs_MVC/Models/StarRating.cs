using Neetechs.Model;
using System.ComponentModel.DataAnnotations;

namespace Neetechs_MVC.Models
{
    public class StarRating
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Star { get; set; }
    }
}
