using Neetechs.Model;
using System.ComponentModel.DataAnnotations;

namespace Neetechs_MVC.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
