using Application;
using Application.Common.Behaviours;
using Domain.Repositories;
using FluentValidation;
using Infrastructure;
using Infrastructure.Interceptors;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Quartz;
using Scrutor;

var builder = WebApplication.CreateBuilder(args);

// Configure services
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Ensure database connection
EnsureDatabaseConnection(app);

// Configure the HTTP request pipeline
ConfigurePipeline(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.Scan(
        selector => selector
            .FromAssemblies(Application.AssemblyReference.Assembly)
            .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Repository")))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddQuartz(configure =>
    {
        var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

        configure
            .AddJob<ProcessOutboxMessagesJob>(jobKey)
            .AddTrigger(
                trigger =>
                    trigger.ForJob(jobKey)
                        .WithSimpleSchedule(
                            schedule =>
                                schedule.WithIntervalInSeconds(100)
                                    .RepeatForever()));

#pragma warning disable CS0618 // Type or member is obsolete
        configure.UseMicrosoftDependencyInjectionJobFactory();
#pragma warning restore CS0618 // Type or member is obsolete
    });

    builder.Services.AddQuartzHostedService();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
    services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
    services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
    services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes: true);

    // Register application db context
    services.AddDbContext<ApplicationDbContext>(
        (sp,options) =>
    {
        options.UseSqlServer(configuration.GetConnectionString("Database"));
        options.AddInterceptors(
            sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>(),
            sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>());

    });
    

    var connectionString = configuration.GetConnectionString("Database");
    services.AddDbContext<ApplicationDbContext>(
        options => options.UseSqlServer(connectionString));

    services
        .AddControllers()
        .AddApplicationPart(Web.AssemblyReference.Assembly);

    // Add MediatR services
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));
}
void EnsureDatabaseConnection(IHost app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        dbContext.Database.CanConnect();
        Console.WriteLine("Database connection successful.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
        throw;
    }
}

void ConfigurePipeline(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();
    app.MapControllers();
}
