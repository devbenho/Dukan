using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    public Task Add(User user)
    {
        dbContext.Add(user);
        return Task.CompletedTask;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        => await dbContext.FindAsync<User>(email, cancellationToken);
    

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await dbContext.FindAsync<User>(id, cancellationToken);
    

    public async Task<EntityEntry<User>> AddAsync(User user, CancellationToken cancellationToken = default)
        => await dbContext.AddAsync(user, cancellationToken);
    

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default) 
        => await dbContext.Update(user).ReloadAsync(cancellationToken);
    

    public Task DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        dbContext.Remove(user);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default) =>
         !await dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
    
}