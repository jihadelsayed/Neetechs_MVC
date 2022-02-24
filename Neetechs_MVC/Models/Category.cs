namespace Neetechs_MVC.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category? SubCategory { get; set; }
    }
}
