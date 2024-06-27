using Microsoft.Extensions.Logging;
using Options;

namespace DefaultNamespace;

public class ExportHandler : ICommandHandler<ExportOptions>
{
    private readonly ILogger<ExportHandler> _logger;

    public ExportHandler(
        ILogger<ExportHandler> logger)
    {
        _logger = logger;
    }
    
    /// <inheritdoc />
    public async Task<int> ExecuteAsync(ExportOptions options, CancellationToken cancellationToken = default)
    {
        return 0;
    }
}