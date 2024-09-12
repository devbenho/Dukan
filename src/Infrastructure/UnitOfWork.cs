using Domain.Repositories;
using Domain.Shared;
using Infrastructure.Identity;
using Infrastructure.Identity.AuthEvents;
using Infrastructure.Outbox;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace Infrastructure;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public UnitOfWork(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ConvertDomainEventsToOutboxMessages();
        UpdateAuditableEntities();
        HandleAuthEvents();

        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void ConvertDomainEventsToOutboxMessages()
    {
        var outboxMessages = _dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(aggregateRoot =>
            {
                IReadOnlyCollection<IDomainEvent> domainEvents = aggregateRoot.GetDomainEvents();

                aggregateRoot.ClearDomainEvents();

                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOnUtc = DateTime.UtcNow,
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })
            })
            .ToList();
        Console.WriteLine("outboxMessages: " + outboxMessages.Count);
        _dbContext.Set<OutboxMessage>().AddRange(outboxMessages);
    }

    private void UpdateAuditableEntities()
    {
        IEnumerable<EntityEntry<IAuditableEntity>> entries =
            _dbContext
                .ChangeTracker
                .Entries<IAuditableEntity>();

        foreach (EntityEntry<IAuditableEntity> entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(a => a.CreatedOnUtc)
                    .CurrentValue = DateTime.UtcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(a => a.ModifiedOnUtc)
                    .CurrentValue = DateTime.UtcNow;
            }
        }
    }

    private void HandleAuthEvents()
    {
        var authEvents = _dbContext.ChangeTracker
            .Entries<ApplicationUser>()
            .Select(x => x.Entity)
            .SelectMany(user =>
            {
                var authEvents = user.GetAuthEvents();
                var authEventBases = authEvents as IAuthEventBase[] ?? authEvents.ToArray();
                var authEventsList = authEventBases.ToList();
                Console.WriteLine("authEvents: ", authEventsList);
                user.ClearAuthEvents();

                return authEventBases;
            })
            .Select(authEvent => new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOnUtc = DateTime.UtcNow,
                Type = authEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(
                    authEvent,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })
            })
            .ToList();
        Console.WriteLine("authEvents: " + authEvents.Count);
        _dbContext.Set<OutboxMessage>().AddRange(authEvents);
    }
}