namespace DefaultNamespace;

/// <summary>
/// Represents a command handler.
/// </summary>
public interface ICommandHandler<in TOptions>
{
    /// <summary>
    /// Executes the implementation.
    /// </summary>
    /// <param name="options">Options that define the behavior of the implementation.</param>
    /// <param name="cancellationToken">Token observed for cancellation</param>
    /// <returns>Task</returns>
    Task<int> ExecuteAsync(TOptions options, CancellationToken cancellationToken = default);
}