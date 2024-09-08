using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    
    private readonly ApplicationDbContext dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    // use dotnet core identity to manage users
    public async Task Add(User user)
    {
        var result = await dbContext.Users.AddAsync(user);
        
    }

    public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
        => await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    

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