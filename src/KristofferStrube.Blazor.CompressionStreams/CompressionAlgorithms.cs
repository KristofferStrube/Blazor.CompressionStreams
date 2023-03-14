namespace KristofferStrube.Blazor.CompressionStreams;

public enum CompressionAlgorithm
{
    Deflate,
    DeflateRaw,
    Gzip
}

public static class CompressionAlgorithmsExtensions
{
    public static string AsString(this CompressionAlgorithm compressionAlgorithm) => compressionAlgorithm switch
    {
        CompressionAlgorithm.Deflate => "deflate",
        CompressionAlgorithm.DeflateRaw => "deflate-raw",
        CompressionAlgorithm.Gzip => "gzip",
        _ => throw new NotSupportedException($"format {compressionAlgorithm.ToString()} not supported as a Compression Algorithms"),
    };
}