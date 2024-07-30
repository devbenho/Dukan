using Domain.Errors;

namespace Domain.ValueObjects
{
    public sealed class PhoneNumber : ValueObject
    {
        private const int MinLength = 10;

        private PhoneNumber(string value) => Value = value;


        public string Value { get; set; }

        public static Result<PhoneNumber> Create(string phoneNumber) =>
            Result.Create(phoneNumber)
                .Ensure(
                    p => !string.IsNullOrWhiteSpace(p),
                    DomainErrors.PhoneNumber.Empty)
                .Ensure(
                    p => p.Length >= MinLength ,
                    DomainErrors.PhoneNumber.TooLong)
                .Ensure(p=> p.Length <= 15, DomainErrors.PhoneNumber.TooShort)
                .Map(p => new PhoneNumber(p));

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}