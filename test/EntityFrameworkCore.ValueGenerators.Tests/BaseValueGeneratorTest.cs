using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LSymds.EntityFrameworkCore.ValueGenerators.Tests;

public abstract class BaseValueGeneratorTest
{
    protected async Task<TContext> BuildDatabaseAsync<TContext>()
        where TContext : BaseTestDatabaseContext
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<TContext>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var databaseContext = serviceProvider.GetService<TContext>()!;
        await databaseContext.Database.EnsureDeletedAsync();
        await databaseContext.Database.EnsureCreatedAsync();

        return databaseContext;
    }
}
