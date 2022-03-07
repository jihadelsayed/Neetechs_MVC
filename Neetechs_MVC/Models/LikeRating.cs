using Neetechs.Model;
using System.ComponentModel.DataAnnotations;

namespace Neetechs_MVC.Models
{
    public class LikeRating
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public Boolean Like { get; set; }
        public Boolean DisLike { get; set; }
    }
}
