# EntityFrameworkCore.ValueGenerators

Numerous value generators and associated utilities for Entity Framework Core.

## Available Generators

### EntityFrameworkCore.ValueGenerators.NanoId

A [NanoId](https://github.com/ai/nanoid) generator that utilises the [nanoid-net](https://github.com/codeyu/nanoid-net)
library.

#### Usage

Install the relevant package:

```shell
dotnet add package LSymds.EntityFrameworkCore.ValueGenerators.NanoId
```

Configure your model properties to use the new value generator with the default settings:

```csharp
modelBuilder
    .Entity<MyModel>
    .Property(p => p.Id)
    .HasValueGenerator<NanoIdValueGenerator>()
    .ValueGeneratedOnAdd();
```

Or provide your own settings:

```csharp
modelBuilder
    .Entity<MyModel>
    .Property(p => p.Id)
    .HasValueGenerator((_, _) => new NanoIdValueGenerator(
        alphabet: "0123456789-_",
        length: 50,
        prefix: "MM"
    )
    .ValueGeneratedOnAdd();
```


