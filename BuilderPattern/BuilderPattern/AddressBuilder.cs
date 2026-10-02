using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BuilderPattern
{
    public class AddressBuilder
    {
        private string? _street;
        private string? _city;
        private string? _state;
        private string? _zipCode;
        private string? _country;

        public AddressBuilder SetStreet(string street)
        {
            _street = street;
            return this;
        }

        public AddressBuilder SetCity(string city)
        {
            _city = city;
            return this;
        }

        public AddressBuilder SetState(string state)
        {
            _state = state;
            return this;
        }

        public AddressBuilder SetZipCode(string zipCode)
        {
            _zipCode = zipCode;
            return this;
        }

        public AddressBuilder SetCountry(string country)
        {
            _country = country;
            return this;
        }

        public Address Build()
        {
            if (string.IsNullOrWhiteSpace(_street))
                throw new InvalidOperationException("Street is required.");

            if (string.IsNullOrWhiteSpace(_city))
                throw new InvalidOperationException("City is required.");

            if (string.IsNullOrWhiteSpace(_state))
                throw new InvalidOperationException("State is required.");

            if (string.IsNullOrWhiteSpace(_zipCode))
                throw new InvalidOperationException("Zip code is required.");

            if (string.IsNullOrWhiteSpace(_country))
                throw new InvalidOperationException("Country is required.");

            return new Address(
                _street,
                _city,
                _state,
                _zipCode,
                _country);
        }
    }
}
