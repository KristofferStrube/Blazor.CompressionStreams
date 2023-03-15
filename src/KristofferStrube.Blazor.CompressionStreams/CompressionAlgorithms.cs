namespace KristofferStrube.Blazor.CompressionStreams;

public enum CompressionAlgorithm
{
    Deflate,
    DeflateRaw,
    Gzip
}

public static class CompressionAlgorithmExtensions
{
    public static string AsString(this CompressionAlgorithm compressionAlgorithm) => compressionAlgorithm switch
    {
        CompressionAlgorithm.Deflate => "deflate",
        CompressionAlgorithm.DeflateRaw => "deflate-raw",
        CompressionAlgorithm.Gzip => "gzip",
        _ => throw new NotSupportedException($"Value {compressionAlgorithm} not supported as a Compression Algorithm format."),
    };
}