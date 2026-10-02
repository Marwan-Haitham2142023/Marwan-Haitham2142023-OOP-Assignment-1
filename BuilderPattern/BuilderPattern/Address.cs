using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderPattern
{
    public class Address
    {
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string ZipCode { get; }
        public string Country { get; }

        public Address(
            string street,
            string city,
            string state,
            string zipCode,
            string country)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
            Country = country;
        }
    }
}
