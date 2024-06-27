namespace Options;

/// <summary>
/// Defines monikers for supported compression types.
/// </summary>
public enum CompressionType
{
    /// <summary>
    /// No compression
    /// </summary>
    None,
    
    /// <summary>
    /// GZip compression
    /// </summary>
    GZip,
    
    /// <summary>
    /// Deflate compression
    /// </summary>
    Deflate
}