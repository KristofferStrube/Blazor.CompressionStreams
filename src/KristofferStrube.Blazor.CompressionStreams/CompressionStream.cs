using KristofferStrube.Blazor.Streams;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.CompressionStreams;

public class CompressionStream : BaseJSWrapper, IGenericTransformStream
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="CompressionStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="CompressionStream"/>.</param>
    /// <returns>A wrapper instance for a <see cref="CompressionStream"/>.</returns>
    public static async Task<CompressionStream> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await Task.FromResult(new CompressionStream(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="format">A valid compression format i.e. deflate, deflate-raw or gzip</param>
    /// <returns>A wrapper instance for a <see cref="CompressionStream"/>.</returns>
    public static async Task<CompressionStream> CreateAsync(IJSRuntime jSRuntime, CompressionAlgorithm format)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("createCompressionStream", format.AsString());
        return await Task.FromResult(new CompressionStream(jSRuntime, jSInstance));
    }

    private CompressionStream(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<ReadableStream> GetReadableAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "readable");
        return ReadableStream.Create(JSRuntime, jSInstance);
    }

    public async Task<WritableStream> GetWritableAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "writable");
        return WritableStream.Create(JSRuntime, jSInstance);
    }
}
