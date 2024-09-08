using Domain.Errors;

namespace Domain.ValueObjects
{
    public sealed class Password : ValueObject
    {
        private const int MinLength = 8;

        private Password(string value) => Value = value;

        private Password()
        {
        }

        public string Value { get; set; }

        public static Result<Password> Create(string password) =>
            Result.Create(password)
                .Ensure(
                    p => !string.IsNullOrWhiteSpace(p),
                    DomainErrors.Password.Empty)
                // .Ensure(
                //     p => p.Length >= MinLength,
                //     DomainErrors.Password.TooShort)
                // .Ensure(
                //     p => p.Any(char.IsUpper),
                //     DomainErrors.Password.MissingUppercase)
                // .Ensure(
                //     p => p.Any(char.IsLower),
                //     DomainErrors.Password.MissingLowercase)
                // .Ensure(
                //     p => p.Any(char.IsDigit),
                //     DomainErrors.Password.MissingDigit)
                // .Ensure(
                //     p => p.Any(char.IsSymbol) || p.Any(char.IsPunctuation),
                //     DomainErrors.Password.MissingSpecialCharacter)
                .Map(p => new Password(p));

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}