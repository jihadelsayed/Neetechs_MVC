namespace Neetechs.Model
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public DateTime AddDate = new DateTime();

        public DateTime Date { get; set; } 
        public string Brand { get; set; }
        public int Price { get; set; }


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
