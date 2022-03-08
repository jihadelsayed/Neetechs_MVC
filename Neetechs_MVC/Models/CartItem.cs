using Neetechs.Model;
using System.ComponentModel.DataAnnotations;

namespace Neetechs_MVC.Models
{
    public class CartItem
    {
        
        public int Id { get; set; } //= Guid.NewGuid().ToString();
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
