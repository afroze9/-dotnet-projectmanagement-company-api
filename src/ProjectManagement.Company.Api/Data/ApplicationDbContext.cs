using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Company.Api.Abstractions;
using ProjectManagement.Company.Api.Common;

namespace ProjectManagement.Company.Api.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IDomainEventDispatcher? _dispatcher;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDomainEventDispatcher dispatcher)
        :base(options)
    {
        _dispatcher = dispatcher;
    }

    public DbSet<Domain.Company> Companies => Set<Domain.Company>();
    public DbSet<Domain.Tag> Tags => Set<Domain.Tag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken);

        if (_dispatcher == null)
        {
            return result;
        }

        EntityBase[] entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();

        await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);
        return result;
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}