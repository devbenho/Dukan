namespace Domain.Errors;

public static class DomainErrors
{
    public static class User
    {
        public static readonly Error EmailAlreadyInUse = new(
            "Member.EmailAlreadyInUse",
            "The specified email is already in use");

        public static readonly Func<Guid, Error> NotFound = id => new Error(
            "Member.NotFound",
            $"The member with the identifier {id} was not found.");

        public static readonly Error InvalidCredentials = new(
            "Member.InvalidCredentials",
            "The provided credentials are invalid");

        public static readonly Error EmailIsRequired = new(
            "User.EmailIsRequired",
            "Email is required");
    }
    public static class Address
    {
        public static readonly Error Empty = new(
            "Address.Empty",
            "Address is empty");

        public static readonly Error TooLong = new(
            "Address.TooLong",
            "Address is too long");

        // street address is empty
        public static readonly Error StreetAddressEmpty = new(
            "Address.StreetAddressEmpty",
            "Street address is empty");

        // street address is too long
        public static readonly Error StreetAddressTooLong = new(
            "Address.StreetAddressTooLong",
            "Street address is too long");

        // city is empty
        public static readonly Error CityEmpty = new(
            "Address.CityEmpty",
            "City is empty");

        // city is too long
        public static readonly Error CityTooLong = new(
            "Address.CityTooLong",
            "City is too long");

        // state is empty
        public static readonly Error StateEmpty = new(
            "Address.StateEmpty",
            "State is empty");

        // state is too long
        public static readonly Error StateTooLong = new(
            "Address.StateTooLong",
            "State is too long");

        // country is empty

        public static readonly Error CountryEmpty = new(
            "Address.CountryEmpty",
            "Country is empty");

        // country is too long
        public static readonly Error CountryTooLong = new(
            "Address.CountryTooLong",
            "Country is too long");

        // zip code is empty
        public static readonly Error ZipCodeEmpty = new(
            "Address.ZipCodeEmpty",
            "Zip code is empty");

        // zip code is too long
        public static readonly Error ZipCodeTooLong = new(
            "Address.ZipCodeTooLong",
            "Zip code is too long");

    }

    public static class FirstName
    {
        public static readonly Error Empty = new(
            "FirstName.Empty",
            "First name is empty");

        public static readonly Error TooLong = new(
            "LastName.TooLong",
            "FirstName name is too long");
        
        public static readonly Error FirstNameIsRequired = new(
            "User.FirstNameIsRequired",
            "First name is required");

    }

    public static class LastName
    {
        public static readonly Error Empty = new(
            "LastName.Empty",
            "Last name is empty");

        public static readonly Error TooLong = new(
            "LastName.TooLong",
            "Last name is too long");
    }

    public static class UserRoles
    {
        public static readonly Error Empty = new(
            "UserRoles.Empty",
            "User roles are empty");

        public static readonly Error TooLong = new(
            "UserRoles.TooLong",
            "User roles are too long");
    }

    public static class UserPermissions
    {
        public static readonly Error Empty = new(
            "UserPermissions.Empty",
            "User permissions are empty");

        public static readonly Error TooLong = new(
            "UserPermissions.TooLong",
            "User permissions are too long");
    }

    public static class UserStatus
    {
        public static readonly Error Empty = new(
            "UserStatus.Empty",
            "User status is empty");

        public static readonly Error TooLong = new(
            "UserStatus.TooLong",
            "User status is too long");
    }
    public static class Email
    {
        public static readonly Error Empty = new(
            "Email.Empty",
            "Email is empty");

        public static readonly Error TooLong = new(
            "Email.TooLong",
            "Email is too long");

        public static readonly Error InvalidFormat = new(
            "Email.InvalidFormat",
            "Email format is invalid");
    }
    
    public static class Username
    {
        public static readonly Error Empty = new(
            "Username.Empty",
            "Username is empty");

        public static readonly Error TooLong = new(
            "Username.TooLong",
            "Username is too long");
        
        
        public static readonly Error InvalidFormat = new(
            "Username.InvalidFormat",
            "Username format is invalid");

    }
    public static class UserRole
    {
        public static readonly Error Empty = new(
            "UserRole.Empty",
            "User role is empty");

        public static readonly Error TooLong = new(
            "UserRole.TooLong",
            "User role is too long");
    }
    
    public static class PhoneNumber
    {
        public static readonly Error Empty = new(
            "PhoneNumber.Empty",
            "Phone number is empty");

        public static readonly Error TooLong = new("PhoneNumber.TooLong",
            "Phone number is too long");

        public static readonly Error TooShort = new("PhoneNumber.TooShort",
            "Phone number is too short");

        public static readonly Error InvalidFormat = new("PhoneNumber.InvalidFormat", "Phone number format is invalid");
    }
    public static class Password
    {
        public static readonly Error Empty = new Error(
            "Password.Empty",
            "Password is empty");

        public static readonly Error TooShort = new Error(
            "Password.TooShort",
            "Password is too short");

        public static readonly Error InvalidFormat = new Error(
            "Password.InvalidFormat",
            "Password format is invalid");

        public static readonly Error MissingUppercase = new Error(
            "Password.MissingUppercase",
            "Password must contain at least one uppercase letter.");

        public static readonly Error MissingLowercase = new Error(
            "Password.MissingLowercase",
            "Password must contain at least one lowercase letter.");

        public static readonly Error MissingDigit = new Error(
            "Password.MissingDigit",
            "Password must contain at least one digit.");

        public static readonly Error MissingSpecialCharacter = new Error(
            "Password.MissingSpecialCharacter",
            "Password must contain at least one special character.");
    }    

}

