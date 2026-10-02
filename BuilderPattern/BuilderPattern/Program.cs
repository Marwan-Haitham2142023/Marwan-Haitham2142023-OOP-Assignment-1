namespace BuilderPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Address billingAddress = new AddressBuilder()
    .SetStreet("Main Street")
    .SetCity("Cairo")
    .SetState("Cairo")
    .SetZipCode("11511")
    .SetCountry("Egypt")
    .Build();

            Address shippingAddress = new AddressBuilder()
                .SetStreet("Second Street")
                .SetCity("Giza")
                .SetState("Giza")
                .SetZipCode("12511")
                .SetCountry("Egypt")
                .Build();

            Order order = new OrderBuilder()
                .SetOrderDate(DateTime.Now)
                .SetPaymentMethod("Credit Card")
                .SetCurrency("EGP")
                .SetSubTotal(1000m)
                .SetDiscountAmount(100m)
                .SetTaxAmount(90m)
                .SetTotalAmount(990m)
                .Build();

            Final_Invoice invoice = new Final_InvoiceBuilder()
                .SetInvoiceId(1001)
                .SetCustomerName("Marwan")
                .SetCustomerEmail("marwan@example.com")
                .SetCustomerPhone("01012345678")
                .SetBillingAddress(billingAddress)
                .SetShippingAddress(shippingAddress)
                .SetOrder(order)
                .Build();
            Final_InvoiceBuilder.DisplayInvoice(invoice);
        }
    }
}
