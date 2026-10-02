using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderPattern
{
    public class InvoiceBuilder
    {
        private int? _invoiceId;
        private string? _customerName;
        private string? _customerEmail;
        private string? _customerPhone;

        private Address? _billingAddress;
        private Address? _shippingAddress;
        private Order? _order;

        public InvoiceBuilder SetInvoiceId(int invoiceId)
        {
            _invoiceId = invoiceId;
            return this;
        }

        public InvoiceBuilder SetCustomerName(string name)
        {
            _customerName = name;
            return this;
        }

        public InvoiceBuilder SetCustomerEmail(string email)
        {
            _customerEmail = email;
            return this;
        }

        public InvoiceBuilder SetCustomerPhone(string phone)
        {
            _customerPhone = phone;
            return this;
        }

        public InvoiceBuilder SetBillingAddress(Address address)
        {
            _billingAddress = address;
            return this;
        }

        public InvoiceBuilder SetShippingAddress(Address address)
        {
            _shippingAddress = address;
            return this;
        }

        public InvoiceBuilder SetOrder(Order order)
        {
            _order = order;
            return this;
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

