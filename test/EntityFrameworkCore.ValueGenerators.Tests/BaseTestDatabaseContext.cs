using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSymds.EntityFrameworkCore.ValueGenerators.Tests;

public class TestModel
{
    public string Id { get; set; }

    public string? Name { get; set; }
}

public abstract class BaseTestDatabaseContext : DbContext
{
    public required DbSet<TestModel> TestModels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"DataSource=file:{Guid.NewGuid().ToString()}?mode=memory");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestModel>(ConfigureEntity);
    }

    protected abstract void ConfigureEntity(EntityTypeBuilder<TestModel> builder);
}
