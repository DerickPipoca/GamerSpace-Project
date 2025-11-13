using GamerSpace.Domain.Bases;

namespace GamerSpace.Domain.Entities
{
    public class Address : EntityBase
    {
        public string PostalCode { get; private set; } = string.Empty;
        public string Country { get; private set; } = string.Empty;
        public string State { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string Street { get; private set; } = string.Empty;
        public string Number { get; private set; } = string.Empty;

        public long UserId { get; private set; }
        public User User { get; private set; } = null!;

        private Address() { }

        public Address(long userId, string postalCode, string country, string state, string city, string street, string number)
        {
            if (userId < 0)
                throw new ArgumentException("User id is invalid.");

            if (String.IsNullOrWhiteSpace(postalCode))
                throw new ArgumentException("Postal code is required.");

            if (String.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country is required.");

            if (String.IsNullOrWhiteSpace(state))
                throw new ArgumentException("State is required.");

            if (String.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City is required.");

            if (String.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street is required.");

            if (String.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Number is required.");

            UserId = userId;
            PostalCode = postalCode;
            Country = country;
            State = state;
            City = city;
            Street = street;
            Number = number;
        }
    }
}