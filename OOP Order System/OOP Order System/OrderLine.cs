using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Order_System
{
    public class OrderLine
    {
        public Product Product;
        public int Quantity;
        public decimal UnitPrice;

        public OrderLine(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            UnitPrice = product.Price;
        }

        public decimal GetLineTotal()
        {
            return UnitPrice * Quantity;
        }
    }
}
