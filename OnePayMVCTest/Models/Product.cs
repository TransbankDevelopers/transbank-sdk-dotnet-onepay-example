namespace OnePayMVCTest.Models
{
    public class Product
    {
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public Product(string imagePath, string name, int price, int quantity)
        {
            ImagePath = imagePath;
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
}