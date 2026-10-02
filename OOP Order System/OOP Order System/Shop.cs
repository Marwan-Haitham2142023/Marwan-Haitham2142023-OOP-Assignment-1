using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Order_System
{
    public class Shop
    {
        private List<Customer> _customers = new List<Customer>();
        private List<Product>  _products = new List<Product>();
        private List<Order>    _orders = new List<Order>();

        public List<Customer> Customers { get { return _customers; } }
        public List<Product> Products { get { return _products; } }
        public List<Order> Orders { get { return _orders; } }

        public Customer FindCustomer(int id)
        {
            return _customers.FirstOrDefault(c => c.Id == id); // I searched about this function
        }

        public Product FindProduct(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public Order FindOrder(int id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public void AddCustomer(int id, string name, string email, string city, bool isVip)
        {
            if (FindCustomer(id) != null)
                throw new InvalidOperationException($"customer id {id} already exists.");
            _customers.Add(new Customer(id, name, email, city, isVip));
        }

        public void AddProduct(int id, string name, decimal price, int stock)
        {
            if (FindProduct(id) != null)
                throw new InvalidOperationException($"product id {id} already exists.");
            _products.Add(new Product(id, name, price, stock));
        }

        public Order CreateOrder(int orderId, int customerId, DateTime date)
        {
            if (FindOrder(orderId) != null)
                throw new InvalidOperationException($"order id {orderId} already exists.");

            Customer customer = FindCustomer(customerId);
            if (customer == null)
                throw new InvalidOperationException($"customer id {customerId} not found.");

            Order order = new Order(orderId, customer, date);
            _orders.Add(order);
            return order;
        }

        public void AddLineToOrder(int orderId, int productId, int quantity)
        {
            Order order = FindOrder(orderId);
            if (order == null)
                throw new InvalidOperationException($"order id {orderId} not found.");

            Product product = FindProduct(productId);
            if (product == null)
                throw new InvalidOperationException($"product id {productId} not found.");

            order.AddLine(product, quantity);
        }

        public void PayOrder(int orderId)
        {
            Order order = FindOrder(orderId);
            if (order == null)
                throw new InvalidOperationException($"order id {orderId} not found.");
            order.MarkPaid();
        }

        public decimal GetPaidSalesTotal()
        {
            return _orders.Where(o => o.isPaid).Sum(o => o.GetTotal());
        }
    }
}
