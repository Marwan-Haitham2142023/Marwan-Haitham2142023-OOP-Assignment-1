using System.Globalization;

namespace OOP_Order_System
{

    public class Program
    {
        public static void Main()
        {
            

            Console.WriteLine("Object-Oriented Order System");

            Shop shop = new Shop();
            SeedSampleData(shop);
            RunDemoScenario(shop);

            ConsoleMenu menu = new ConsoleMenu(shop);
            menu.PrintCustomers();
            menu.PrintProducts();
            menu.PrintAllOrders();
            Console.WriteLine($"\nPaid sales total after demo: {shop.GetPaidSalesTotal():F2}");

            menu.Run();
        }

        private static void SeedSampleData(Shop shop)
        {
            shop.AddCustomer(1, "Mona Ali", "mona@example.com", "Cairo", true);
            shop.AddCustomer(2, "Omar Hassan", "omar@example.com", "Alexandria", false);
            shop.AddCustomer(3, "Sara Nabil", "sara@example.com", "Giza", false);

            shop.AddProduct(101, "USB Cable", 50m, 100);
            shop.AddProduct(102, "Wireless Mouse", 250m, 40);
            shop.AddProduct(103, "Mechanical Keyboard", 1200m, 15);
            shop.AddProduct(104, "Laptop Stand", 400m, 25);
        }

        private static void RunDemoScenario(Shop shop)
        {
            shop.CreateOrder(1001, 1, new DateTime(2026, 9, 15));
            shop.AddLineToOrder(1001, 101, 2);
            shop.AddLineToOrder(1001, 102, 1);
            shop.PayOrder(1001);

            shop.CreateOrder(1002, 2, new DateTime(2026, 9, 15));
            shop.AddLineToOrder(1002, 103, 1);
            shop.AddLineToOrder(1002, 104, 1);

            shop.CreateOrder(1003, 3, new DateTime(2026, 9, 16));
            shop.AddLineToOrder(1003, 101, 5);
            shop.PayOrder(1003);
        }
    }
}










