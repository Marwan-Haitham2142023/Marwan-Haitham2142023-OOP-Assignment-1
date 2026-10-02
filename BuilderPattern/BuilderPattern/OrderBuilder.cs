using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderPattern
{
    public class OrderBuilder
    {
        private DateTime? _orderDate;
        private string? _paymentMethod;
        private string? _currency;

        private decimal? _subTotal;
        private decimal _discountAmount;
        private decimal _taxAmount;
        private decimal? _totalAmount;

        public OrderBuilder SetOrderDate(DateTime orderDate)
        {
            _orderDate = orderDate;
            return this;
        }

        public OrderBuilder SetPaymentMethod(string paymentMethod)
        {
            _paymentMethod = paymentMethod;
            return this;
        }

        public OrderBuilder SetCurrency(string currency)
        {
            _currency = currency;
            return this;
        }

        public OrderBuilder SetSubTotal(decimal subTotal)
        {
            _subTotal = subTotal;
            return this;
        }

        public OrderBuilder SetDiscountAmount(decimal discount)
        {
            _discountAmount = discount;
            return this;
        }

        public OrderBuilder SetTaxAmount(decimal tax)
        {
            _taxAmount = tax;
            return this;
        }

        public OrderBuilder SetTotalAmount(decimal total)
        {
            _totalAmount = total;
            return this;
        }

        public Order Build()
        {
            if (!_orderDate.HasValue)
                throw new InvalidOperationException("Order date is required.");

            if (string.IsNullOrWhiteSpace(_paymentMethod))
                throw new InvalidOperationException("Payment method is required.");

            if (string.IsNullOrWhiteSpace(_currency))
                throw new InvalidOperationException("Currency is required.");

            if (!_subTotal.HasValue)
                throw new InvalidOperationException("Subtotal is required.");

            if (!_totalAmount.HasValue)
                throw new InvalidOperationException("Total amount is required.");

            return new Order(
                _orderDate.Value,
                _paymentMethod,
                _currency,
                _subTotal.Value,
                _discountAmount,
                _taxAmount,
                _totalAmount.Value);
        }
    }
}
