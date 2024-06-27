namespace SqlDump.Types;

/// <summary>
/// Creates type handlers.
/// </summary>
public static class TypeHandlerFactory
{
    private static readonly Dictionary<Type, ITypeHandler> Cached = new();

    /// <summary>
    /// Creates a handler for the given type.
    /// </summary>
    /// <param name="nullableHint">A hint as to the nullability of the type.</param>
    /// <typeparam name="T">Type</typeparam>
    /// <returns><see cref="ITypeHandler"/></returns>
    /// <exception cref="NotSupportedException">The type is not supported.</exception>
    public static ITypeHandler Create<T>(bool? nullableHint = null) => Create(typeof(T), nullableHint);
    
    /// <summary>
    /// Creates a handler for the given type.
    /// </summary>
    /// <param name="type">Type being managed.</param>
    /// <param name="nullableHint">A hint as to the nullability of the type.</param>
    /// <returns><see cref="ITypeHandler"/></returns>
    public static ITypeHandler Create(Type type, bool? nullableHint = null)
    {
        if (Cached.TryGetValue(type, out var cached))
            return cached;

        cached = CreateInstance(type, nullableHint);
        Cached.Add(type, cached);

        return cached;
    }

    private static ITypeHandler CreateInstance(Type type, bool? nullableHint)
    {
        if (type == typeof(string))
        {
            return new DelegatedTypeHandler<string>(s => s, nullableHint ?? true);
        }

        if (type is { IsClass: false, IsGenericType: true, GenericTypeArguments.Length: 1 } &&
            type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            var underlyingValueType = type.GenericTypeArguments[0];

            if (!IsParsable(underlyingValueType))
            {
                throw new NotSupportedException(type.ToString());
            }
            
            var handlerType = typeof(ParsableNullValueTypeHandler<>).MakeGenericType(underlyingValueType);
            return (ITypeHandler)Activator.CreateInstance(handlerType)!;
        }

        if (IsParsable(type))
        {
            var handlerType = typeof(ParsableTypeHandler<>).MakeGenericType(type);
            var parameters = new object[] { nullableHint ?? type.IsClass };
            return (ITypeHandler)Activator.CreateInstance(handlerType, parameters)!;
        }

        throw new NotSupportedException(type.ToString());
    }

    private static bool IsParsable(Type type)
    {
        return type
            .GetInterfaces()
            .Any(implements => implements is
            {
                IsGenericType: true,
                GenericTypeArguments.Length: 1
            } && implements.GetGenericTypeDefinition() == typeof(IParsable<>));
    }
}