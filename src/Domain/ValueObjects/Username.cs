using Domain.Errors;

namespace Domain.ValueObjects;

public sealed class Username : ValueObject
{
    private const int MaxLength = 50;

    private Username(string value) => Value = value;

    private Username()
    {
    }

    public string Value { get; set; }

    public static Result<Username> Create(string username) =>
        Result.Create(username)
            .Ensure(
                u => !string.IsNullOrWhiteSpace(u),
                DomainErrors.Username.Empty)
            .Ensure(
                u => u.Length <= MaxLength,
                DomainErrors.Username.TooLong)
            .Ensure(
                u => u.All(c => !char.IsWhiteSpace(c)),
                DomainErrors.Username.InvalidFormat)
            .Map(u => new Username(u));

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}