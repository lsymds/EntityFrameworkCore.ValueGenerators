using LSymds.EntityFrameworkCore.ValueGenerators.NanoId;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shouldly;

namespace LSymds.EntityFrameworkCore.ValueGenerators.Tests.NanoId;

public class NanoIdValueGeneratorTests
{
    public class ManuallyConfiguredProperties : BaseValueGeneratorTest
    {
        public class DefaultValueGeneratorContext : BaseTestDatabaseContext
        {
            protected override void ConfigureEntity(EntityTypeBuilder<TestModel> builder)
            {
                builder
                    .Property(p => p.Id)
                    .HasValueGenerator<NanoIdValueGenerator>()
                    .ValueGeneratedOnAdd();
            }
        }

        [Fact]
        public async Task CorrectlyGeneratesAndPersistsAPropertyConfiguredToUseTheGenerator()
        {
            // Arrange.
            var context = await BuildDatabaseAsync<DefaultValueGeneratorContext>();

            // Act.
            context.TestModels.Add(new TestModel());
            await context.SaveChangesAsync();

            // Assert.
            var persistedModel = await context.TestModels.FirstAsync();
            persistedModel.Id.Length.ShouldBe(NanoIdValueGenerator.DefaultLength);
        }

        public class CustomOptionsValueGeneratorContext : BaseTestDatabaseContext
        {
            protected override void ConfigureEntity(EntityTypeBuilder<TestModel> builder)
            {
                builder
                    .Property(p => p.Id)
                    .HasValueGenerator(
                        (_, _) => new NanoIdValueGenerator("0123456789-_", 10, "USR")
                    )
                    .ValueGeneratedOnAdd();
            }
        }

        [Fact]
        public async Task CorrectlyGeneratesAndPersistsAPropertyConfiguredToUseTheCustomisedGenerator()
        {
            // Arrange.
            var context = await BuildDatabaseAsync<CustomOptionsValueGeneratorContext>();

            // Act.
            context.TestModels.Add(new TestModel());
            await context.SaveChangesAsync();

            // Assert.
            var persistedModel = await context.TestModels.FirstAsync();
            persistedModel.Id.Length.ShouldBe(13);
            persistedModel.Id.ShouldStartWith("USR");
            persistedModel
                .Id[2..]
                .ToCharArray()
                .ShouldNotContain(i => "abcdefghijklmnopqrstuvwxyz".IndexOf(i) > -1);
        }
    }
}
