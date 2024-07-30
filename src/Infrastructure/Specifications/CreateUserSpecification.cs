using Domain.Entities;

namespace Infrastructure.Specifications;

public class CreateUserSpecification(string email) : Specification<User>(u => u.Email.Value == email); 