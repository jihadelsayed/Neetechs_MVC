using Microsoft.AspNetCore.Mvc.Rendering;
using Neetechs_MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Neetechs.Model
{
    public class Product
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? UserId { get; set; }
        public Profile? Profile { get; set; }

        public DateTime? AddDate = DateTime.UtcNow;
        
        public DateTime Date { get; set; }

        public string? Description { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public int? Review { get; set; }
        public string? FileName { get; set; }
        public byte[]? File { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; }

    }
    public class Laptop : Product
    {
        public string GPUType { get; set; }
        public string Model { get; set; }

    }
    public class Mobile : Product
    {
        public int NumberOfCamera { get; set; }
        public string Model { get; set; }
    }

}
