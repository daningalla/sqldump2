using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace SqlDump.Collections;

/// <summary>
/// A collection that has dictionary and index access.
/// </summary>
/// <typeparam name="TKey">Key type</typeparam>
/// <typeparam name="TValue">Value type</typeparam>
public class ReadOnlyIndex<TKey, TValue> : 
    IReadOnlyDictionary<TKey, TValue> where TKey : notnull
{
    private readonly IEqualityComparer<TKey> _keyComparer;
    private readonly Dictionary<TKey, TValue> _dictionary;
    private readonly KeyValuePair<TKey, TValue>[] _array;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlyIndex{TKey,TValue}"/> type.
    /// </summary>
    /// <param name="entries">Entries of the index. The enumerated order determines the order of the index.</param>
    /// <param name="keyComparer">Key comparer implementation.</param>
    public ReadOnlyIndex(
        IEnumerable<KeyValuePair<TKey, TValue>> entries,
        IEqualityComparer<TKey>? keyComparer = null)
    {
        _array = entries.ToArray();
        _dictionary = _array.ToDictionary(
            kv => kv.Key,
            kv => kv.Value,
            _keyComparer = keyComparer ?? EqualityComparer<TKey>.Default);
    }

    /// <inheritdoc />
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _dictionary.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public int Count => _array.Length;

    /// <summary>
    /// Gets the index of the given key.
    /// </summary>
    /// <param name="key">Key to find.</param>
    /// <returns>The index or <c>-1</c> if the value is not found.</returns>
    public int IndexOfKey(TKey key) => IndexOf(kv => _keyComparer.Equals(key, kv.Key));

    /// <inheritdoc />
    public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

    /// <inheritdoc />
    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) => 
        _dictionary.TryGetValue(key, out value);

    /// <inheritdoc />
    public TValue this[TKey key] => _dictionary[key];

    /// <summary>
    /// Gets the value at the given index.
    /// </summary>
    /// <param name="index">Index</param>
    public TValue this[int index] => _array[index].Value;

    /// <summary>
    /// Gets the key at the given index.
    /// </summary>
    /// <param name="index">Index</param>
    /// <returns></returns>
    public TKey KeyAt(int index) => _array[index].Key;

    /// <inheritdoc />
    public IEnumerable<TKey> Keys => _dictionary.Keys;

    /// <inheritdoc />
    public IEnumerable<TValue> Values => _dictionary.Values;

    /// <inheritdoc />
    public override string ToString() => $"{Count}";

    private int IndexOf(Predicate<KeyValuePair<TKey, TValue>> predicate)
    {
        for (var c = 0; c < _array.Length; c++)
        {
            if (predicate(_array[c]))
                return c;
        }

        return -1;
    }
}