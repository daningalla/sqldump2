using Microsoft.Extensions.Logging;

namespace SqlDump.Options;

public delegate bool OptionsValidatorAction<in TOptions>(ILogger logger, TOptions options);

public sealed class OptionsValidator<TOptions>(
    ILogger<OptionsValidator<TOptions>> logger,
    IEnumerable<OptionsValidatorAction<TOptions>> validators)
{
    /// <summary>
    /// Validates the options using the actions registered.
    /// </summary>
    /// <param name="options">Options to validate.</param>
    /// <returns>Whether validation succeeded.</returns>
    public bool Validate(TOptions options)
    {
        return validators.All(validator => validator(logger, options));
    }
}