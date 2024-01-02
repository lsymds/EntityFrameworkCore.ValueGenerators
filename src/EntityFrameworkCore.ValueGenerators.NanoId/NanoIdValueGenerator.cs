using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using NanoidDotNet;

namespace LSymds.EntityFrameworkCore.ValueGenerators.NanoId;

/// <summary>
/// An Entity Framework Core <see cref="ValueGenerator"/> implementation that generates a NanoID.
/// </summary>
public class NanoIdValueGenerator : ValueGenerator
{
    public const string DefaultAlphabet =
        "_-0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const int DefaultLength = 21;

    private readonly string _alphabet;
    private readonly int _length;
    private readonly string _prefix;

    /// <summary>
    /// Initializes a new instance of the <see cref="NanoIdValueGenerator"/> with default options.
    /// </summary>
    public NanoIdValueGenerator()
        : this(DefaultAlphabet, DefaultLength) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="NanoIdValueGenerator"/> with customized options.
    /// </summary>
    /// <param name="alphabet">
    /// The alphabet to use within the ID. Defaults to <see cref="DefaultAlphabet"/>.
    /// </param>
    /// <param name="length">
    /// <para>
    /// The length of the ID to generate. Defaults to <see cref="DefaultLength"/>.
    /// </para>
    /// <para>
    /// If your database and the column this value generator is being applied to is using a case sensitive collation,
    /// you may want to increase the length that is used in order to deal with the higher probability of a collision.
    /// Check your collision chances at https://zelark.github.io/nano-id-cc/.
    /// </para>
    /// <para>
    /// This length determines the length of the randomly generated id WITHOUT the prefix. If you specify a length
    /// of 10 and a prefix of FOO, then you will get a generated value of 13 characters.
    /// </para>
    /// </param>
    /// <param name="prefix">The prefix to place before any generated ids.</param>
    public NanoIdValueGenerator(
        string alphabet = DefaultAlphabet,
        int length = DefaultLength,
        string prefix = ""
    )
    {
        _alphabet = alphabet;
        _length = length;
        _prefix = prefix;
    }

    /// <summary>
    /// Generates a new NanoID according to the options provided.
    /// </summary>
    /// <param name="entry">The entity entry.</param>
    /// <returns>The generated NanoID.</returns>
    protected override object? NextValue(EntityEntry entry) =>
        $"{_prefix}{Nanoid.Generate(_alphabet, _length)}";

    /// <summary>
    /// Gets that this value generator does not generate temporary values.
    /// </summary>
    public override bool GeneratesTemporaryValues => false;
}
