using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Order_System
{
    public class Customer
    {
        private const decimal _VipDiscountRate = 0.10m;

        public int Id;
        public string Name;
        public string Email;
        public string City;
        public bool isVip;

        public Customer(int id, string name, string email, string city, bool isVip)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("customer name is required.");
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                throw new ArgumentException("customer email is not valid.");

            Id = id;
            Name = name;
            Email = email;
            City = city;
            this.isVip = isVip;
        }

        public decimal GetDiscountRate()
        {
            return isVip ? _VipDiscountRate : 0m;
        }

        public override string ToString()
        {
            return $"#{Id}  {Name}  <{Email}>  {City}  vip={(isVip ? "yes" : "no")}";
        }
    }
}
