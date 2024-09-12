using Domain.Shared;
using Infrastructure.Identity.AuthEvents;
using Infrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;

// Add this using directive

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublisher publisher) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (var outboxMessage in messages)
        {
            // Deserialize to AuthEventBase first
            var authEvent = DeserializeEvent<IAuthEventBase>(outboxMessage.Content);
            if (authEvent != null)
            {
                await publisher.Publish(authEvent, context.CancellationToken);
                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
                continue; // Skip to the next message
            }

            // If not an AuthEventBase, try to deserialize as IDomainEvent
            var domainEvent = DeserializeEvent<IDomainEvent>(outboxMessage.Content);
            if (domainEvent != null)
            {
                await publisher.Publish(domainEvent, context.CancellationToken);
                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            }
        }

        await dbContext.SaveChangesAsync();
    }

    private T? DeserializeEvent<T>(string content)
    {
        return JsonConvert.DeserializeObject<T>(
            content,
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
    }
}