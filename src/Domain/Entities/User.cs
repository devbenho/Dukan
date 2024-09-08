using Domain.Events;

namespace Domain.Entities
{
    using Domain.Shared;

    public sealed class User : AggregateRoot, IAuditableEntity
    {
        
        public FirstName FirstName { get; init; }
        public LastName LastName { get; init; } 
        public Email Email { get; init; } 
        public Username UserName { get; init; } 
        public Password Password { get; init; } 
        public PhoneNumber PhoneNumber { get; init; } 
        public bool IsAdmin { get; init; }
        public bool IsSuperAdmin { get; init; }
        public bool IsActive { get; init; }

        public ICollection<Address> Addresses { get; set; }

        public ICollection<UserRole> Roles { get; set; }
        public DateTimeOffset Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public string? LastModifiedBy { get; set; }

        private User(Guid id, FirstName firstName, LastName lastName, Email email, Username userName, Password password, PhoneNumber phoneNumber, bool isAdmin, bool isSuperAdmin, bool isActive, string? createdBy) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            Password = password;
            PhoneNumber = phoneNumber;
            IsAdmin = isAdmin;
            IsSuperAdmin = isSuperAdmin;
            IsActive = isActive;
            Created = DateTimeOffset.UtcNow;
            CreatedBy = createdBy;
            LastModified = DateTimeOffset.UtcNow;
            LastModifiedBy = createdBy;
            Addresses = new List<Address>();
            Roles = new List<UserRole>();
        }

        public static User Create(Guid id, FirstName firstName, LastName lastName, Email email, Username userName,
            Password password, PhoneNumber phoneNumber, bool isAdmin, bool isSuperAdmin, bool isActive, string? createdBy)
        {
            var user = new User(id, firstName, lastName, email, userName, password, phoneNumber, isAdmin, isSuperAdmin,
                isActive, createdBy);
            user.RaiseDomainEvent(new UserRegisteredDomainEvent(new Guid(), user.Id));
            return user;
        }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
    }
}
