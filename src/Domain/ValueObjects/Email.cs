using Domain.Errors;

namespace Domain.ValueObjects;

public sealed class Email : ValueObject
{
    private const int MaxLength = 255;

    private Email(string value) => Value = value;

    private Email()
    {
    }

    public string Value { get; set; }

    public static Result<Email> Create(string email) =>
        Result.Create(email)
            .Ensure(
                e => !string.IsNullOrWhiteSpace(e),
                DomainErrors.Email.Empty)
            .Ensure(
                e => e.Length <= MaxLength,
                DomainErrors.Email.TooLong)
            .Ensure(
                e => e.Split('@').Length == 2,
                DomainErrors.Email.InvalidFormat)
            .Map(e => new Email(e));

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
