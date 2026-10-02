using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderPattern
{
    public class Final_InvoiceBuilder
    {
        private int? _invoiceId;
        private string? _customerName;
        private string? _customerEmail;
        private string? _customerPhone;

        private Address? _billingAddress;
        private Address? _shippingAddress;
        private Order? _order;

        public Final_InvoiceBuilder SetInvoiceId(int invoiceId)
        {
            _invoiceId = invoiceId;
            return this;
        }

        public Final_InvoiceBuilder SetCustomerName(string name)
        {
            _customerName = name;
            return this;
        }

        public Final_InvoiceBuilder SetCustomerEmail(string email)
        {
            _customerEmail = email;
            return this;
        }

        public Final_InvoiceBuilder SetCustomerPhone(string phone)
        {
            _customerPhone = phone;
            return this;
        }

        public Final_InvoiceBuilder SetBillingAddress(Address address)
        {
            _billingAddress = address;
            return this;
        }

        public Final_InvoiceBuilder SetShippingAddress(Address address)
        {
            _shippingAddress = address;
            return this;
        }

        
        public Final_InvoiceBuilder SetOrder(Order order)
        {
            _order = order;
            return this;
        }

        public static void DisplayInvoice(Final_Invoice invoice)
        {
            Console.WriteLine("========== INVOICE ==========");

            Console.WriteLine($"Invoice ID: {invoice.InvoiceId}");
            Console.WriteLine($"Customer Name: {invoice.CustomerName}");
            Console.WriteLine($"Customer Email: {invoice.CustomerEmail}");
            Console.WriteLine($"Customer Phone: {invoice.CustomerPhone}");

            Console.WriteLine("\n--- Billing Address ---");
            Console.WriteLine($"Street: {invoice.BillingAddress.Street}");
            Console.WriteLine($"City: {invoice.BillingAddress.City}");
            Console.WriteLine($"State: {invoice.BillingAddress.State}");
            Console.WriteLine($"Zip Code: {invoice.BillingAddress.ZipCode}");
            Console.WriteLine($"Country: {invoice.BillingAddress.Country}");

            Console.WriteLine("\n--- Shipping Address ---");
            Console.WriteLine($"Street: {invoice.ShippingAddress.Street}");
            Console.WriteLine($"City: {invoice.ShippingAddress.City}");
            Console.WriteLine($"State: {invoice.ShippingAddress.State}");
            Console.WriteLine($"Zip Code: {invoice.ShippingAddress.ZipCode}");
            Console.WriteLine($"Country: {invoice.ShippingAddress.Country}");

            Console.WriteLine("\n--- Order Information ---");
            Console.WriteLine($"Order Date: {invoice.Order.OrderDate}");
            Console.WriteLine($"Payment Method: {invoice.Order.PaymentMethod}");
            Console.WriteLine($"Currency: {invoice.Order.Currency}");
            Console.WriteLine($"Subtotal: {invoice.Order.SubTotal}");
            Console.WriteLine($"Discount: {invoice.Order.DiscountAmount}");
            Console.WriteLine($"Tax: {invoice.Order.TaxAmount}");
            Console.WriteLine($"Total: {invoice.Order.TotalAmount}");

            Console.WriteLine("=============================");
        }

        public Final_Invoice Build()
        {
            if (!_invoiceId.HasValue)
                throw new InvalidOperationException("Invoice ID is required.");

            if (string.IsNullOrWhiteSpace(_customerName))
                throw new InvalidOperationException("Customer name is required.");

            if (string.IsNullOrWhiteSpace(_customerEmail))
                throw new InvalidOperationException("Customer email is required.");

            if (_billingAddress == null)
                throw new InvalidOperationException("Billing address is required.");

            if (_shippingAddress == null)
                throw new InvalidOperationException("Shipping address is required.");

            if (_order == null)
                throw new InvalidOperationException("Order information is required.");

            return new Final_Invoice(
                _invoiceId.Value,
                _customerName,
                _customerEmail,
                _customerPhone,
                _billingAddress,
                _shippingAddress,
                _order);
        }
    }

}
