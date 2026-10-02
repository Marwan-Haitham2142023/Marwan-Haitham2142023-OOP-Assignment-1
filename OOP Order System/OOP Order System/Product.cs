using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Order_System
{
    public class Product
    {
        public int Id;
        public string Name;
        public decimal Price;
        public int Stock;

        public Product(int id, string name, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("product name is required.");
            if (price < 0)
                throw new ArgumentException("price cannot be negative.");
            if (stock < 0)
                throw new ArgumentException("stock cannot be negative.");

            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }

        public void RemoveStock(int quantity)
        {
            if (Stock < quantity)
                throw new InvalidOperationException($"not enough stock for product #{Id}.");
            Stock -= quantity;
        }

        public override string ToString()
        {
            return $"#{Id}  {Name}  price={Price:F2}  stock={Stock}";
        }
    }

}
