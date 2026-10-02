using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderPattern
{
    public class Old_Invoice
    {
        public int InvoiceId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }

        // Billing Address
        public Address BillingAddress { get; set; }

        // Shipping Address
        public Address ShippingAddress { get; set; }

        // Order / Payment
        public DateTime OrderDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Currency { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
