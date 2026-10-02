using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Order_System
{
    public class Order
    {
        private const int _MaxLines = 20;

        public int Id;
        public Customer Customer;
        public DateTime Date;
        public bool isPaid;
        public List<OrderLine> Lines = new List<OrderLine>();
        public decimal DiscountRate;

        public Order(int id, Customer customer, DateTime date)
        {
            Id = id;
            Customer = customer;
            Date = date;
            isPaid = false;
            DiscountRate = customer.GetDiscountRate();
        }

        public void AddLine(Product product, int quantity)
        {
            if (isPaid)
                throw new InvalidOperationException("cannot change a paid order.");
            if (Lines.Count >= _MaxLines)
                throw new InvalidOperationException("order has too many lines.");
            if (quantity <= 0)
                throw new ArgumentException("quantity must be positive.");

            product.RemoveStock(quantity);
            Lines.Add(new OrderLine(product, quantity));
        }

        public void MarkPaid()
        {
            if (Lines.Count == 0)
                throw new InvalidOperationException("cannot pay an empty order.");
            if (isPaid)
                throw new InvalidOperationException("order is already paid.");
            isPaid = true;
        }

        public decimal GetSubtotal()
        {
            return Lines.Sum(l => l.GetLineTotal());
        }

        public decimal GetDiscount()
        {
            return GetSubtotal() * DiscountRate;
        }

        public decimal GetTotal()
        {
            return GetSubtotal() - GetDiscount();
        }
    }
}
