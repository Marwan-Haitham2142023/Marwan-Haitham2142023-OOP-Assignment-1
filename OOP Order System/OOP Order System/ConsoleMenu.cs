using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OOP_Order_System
{

    public class ConsoleMenu
    {
        private Shop _shop;

        public ConsoleMenu(Shop shop)
        {
            this._shop = shop;
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                PrintMenu();
                int choice = ReadInt("");

                try
                {
                    switch (choice)
                    {
                        case 1: PrintCustomers(); break;
                        case 2: PrintProducts(); break;
                        case 3: PrintAllOrders(); break;
                        case 4: PrintOneOrder(); break;
                        case 5: CreateOrder(); break;
                        case 6: AddLine(); break;
                        case 7: PayOrder(); break;
                        case 8: Console.WriteLine($"Paid sales total: {_shop.GetPaidSalesTotal():F2}"); break;
                        case 9: AddCustomer(); break;
                        case 10: AddProduct(); break;
                        case 0: Console.WriteLine("Bye."); running = false; break;
                        default: Console.WriteLine("Unknown choice."); break;
                    }
                }
                catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                }
            }
        }

        public void PrintMenu()
        {
            Console.WriteLine("\n---------- MENU ----------");
            Console.WriteLine("1) Print customers");
            Console.WriteLine("2) Print products");
            Console.WriteLine("3) Print all orders");
            Console.WriteLine("4) Print one order by id");
            Console.WriteLine("5) Create order");
            Console.WriteLine("6) Add line to order");
            Console.WriteLine("7) Mark order paid");
            Console.WriteLine("8) Show paid sales total");
            Console.WriteLine("9) Add customer");
            Console.WriteLine("10) Add product");
            Console.WriteLine("0) Exit");
            Console.Write("Choice: ");
        }

        public void PrintCustomers()
        {
            Console.WriteLine($"\n=== CUSTOMERS ({_shop.Customers.Count}) ===");
            foreach (Customer c in _shop.Customers)
                Console.WriteLine(c);
        }

        public void PrintProducts()
        {
            Console.WriteLine($"\n=== PRODUCTS ({_shop.Products.Count}) ===");
            foreach (Product p in _shop.Products)
                Console.WriteLine(p);
        }

        public void PrintAllOrders()
        {
            Console.WriteLine($"\n=== ALL ORDERS ({_shop.Orders.Count}) ===");
            foreach (Order o in _shop.Orders)
                PrintOrder(o);
        }

        public void PrintOrder(Order order)
        {
            Console.WriteLine($"\n=== ORDER #{order.Id} ===");
            Console.WriteLine($"Date: {order.Date:yyyy-MM-dd}");
            Console.WriteLine($"Customer: {order.Customer.Name} (#{order.Customer.Id})");
            Console.WriteLine($"Paid: {(order.isPaid ? "yes" : "no")}");
            Console.WriteLine("Lines:");
            foreach (OrderLine line in order.Lines)
            {
                Console.WriteLine($"  - {line.Product.Name}  x{line.Quantity}  @{line.UnitPrice:F2}  = {line.GetLineTotal():F2}");
            }
            if (order.DiscountRate > 0)
            {
                Console.WriteLine($"Subtotal: {order.GetSubtotal():F2}");
                Console.WriteLine($"Discount ({order.DiscountRate:P0}): -{order.GetDiscount():F2}");
            }
            Console.WriteLine($"TOTAL: {order.GetTotal():F2}");
        }

        private void PrintOneOrder()
        {
            int id = ReadInt("Order id: ");
            Order order = _shop.FindOrder(id);
            if (order == null)
                Console.WriteLine($"ERROR: order id {id} not found.");
            else
                PrintOrder(order);
        }

        private void CreateOrder()
        {
            int orderId = ReadInt("Order id: ");
            int customerId = ReadInt("Customer id: ");
            DateTime date = ReadDate("Date (YYYY-MM-DD): ");
            _shop.CreateOrder(orderId, customerId, date);
        }

        private void AddLine()
        {
            int orderId = ReadInt("Order id: ");
            int productId = ReadInt("Product id: ");
            int quantity = ReadInt("Quantity: ");
            _shop.AddLineToOrder(orderId, productId, quantity);
        }

        private void PayOrder()
        {
            _shop.PayOrder(ReadInt("Order id: "));
        }

        private void AddCustomer()
        {
            int id = ReadInt("Customer id: ");
            string name = ReadString("Name: ");
            string email = ReadString("Email: ");
            string city = ReadString("City: ");
            bool vip = ReadString("VIP? (y/n): ").Trim().ToLower() == "y";
            _shop.AddCustomer(id, name, email, city, vip);
        }

        private void AddProduct()
        {
            int id = ReadInt("Product id: ");
            string name = ReadString("Name: ");
            decimal price = ReadDecimal("Price: ");
            int stock = ReadInt("Stock: ");
            _shop.AddProduct(id, name, price, stock);
        }

        private int ReadInt(string prompt)
        {
            while (true)
            {
                if (prompt != "") Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value))
                    return value;
                Console.Write("Please enter a whole number: ");
                prompt = "";
            }
        }

        private decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal value))
                    return value;
                Console.WriteLine("Please enter a valid number.");
            }
        }

        private DateTime ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime value))
                    return value;
                Console.WriteLine("Please use the format YYYY-MM-DD.");
            }
        }

        private string ReadString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? "";
        }
    }
}
