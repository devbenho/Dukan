using System.Text.Json.Serialization;
using Domain.Errors;
using Domain.Shared;

namespace Domain.ValueObjects;
public sealed class Address : ValueObject
{
    public string Street { get; init; } = null!;
    public string City { get; init; } = null!;
    public string State { get; init; } = null!;
    public string Country { get; init; } = null!;
    public string ZipCode { get; init; } = null!;
    
    public string Type { get; init; } = null!;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return Country;
        yield return ZipCode;
        yield return Type;
    }
    
    private Address()
    {
    }
    [JsonConstructor]
    private Address(string street, string city, string state, string country, string zipCode, string type)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
        Type = type;
    }

    public static Result<Address> Create(string street, string city, string state, string country, string zipCode, string type)
    {
        if (string.IsNullOrWhiteSpace(street))
        {
            return Result.Failure<Address>(DomainErrors.Address.CityEmpty);
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            return Result.Failure<Address>(DomainErrors.Address.CityEmpty);
        }

        if (string.IsNullOrWhiteSpace(state))
        {
            return Result.Failure<Address>(DomainErrors.Address.StateEmpty);
        }

        if (string.IsNullOrWhiteSpace(country))
        {
            return Result.Failure<Address>(DomainErrors.Address.CountryEmpty);
        }

        
        return string.IsNullOrWhiteSpace(zipCode) ? Result.Failure<Address>(DomainErrors.Address.ZipCodeEmpty) : new Address(street, city, state, country, zipCode, string.IsNullOrWhiteSpace(type) ? "Home" : type);
    }
}

