using FreeCourse.Services.Order.Domain.Core;
using System.Collections.Generic;


namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    //[Owned]
    public class Address : ValueObject
    {
        public Address(string city, string district, string street, string zipCode, string addressLine)
        {
            City = city;
            District = district;
            Street = street;
            ZipCode = zipCode;
            AddressLine = addressLine;
        }

        public string City { get; private set; }
        public string District { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public string AddressLine { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return City;
            yield return District;
            yield return Street;
            yield return ZipCode;
            yield return AddressLine;
        }
    }
}
