using Neetechs.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Neetechs_MVC.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public Profile User { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }
        public double Price { get; set; }

        [ForeignKey("MovieId")]
        public int MovieId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
