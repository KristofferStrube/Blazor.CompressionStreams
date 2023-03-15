using KristofferStrube.Blazor.Streams;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.CompressionStreams;

public class DecompressionStream : BaseJSWrapper, IGenericTransformStream
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="DecompressionStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="DecompressionStream"/>.</param>
    /// <returns>A wrapper instance for a <see cref="DecompressionStream"/>.</returns>
    public static async Task<DecompressionStream> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await Task.FromResult(new DecompressionStream(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="format">A valid compression format i.e. deflate, deflate-raw or gzip</param>
    /// <returns>A wrapper instance for a <see cref="DecompressionStream"/>.</returns>
    public static async Task<DecompressionStream> CreateAsync(IJSRuntime jSRuntime, CompressionAlgorithm format)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("createDecompressionStream", format.AsString());
        return await Task.FromResult(new DecompressionStream(jSRuntime, jSInstance));
    }

    private DecompressionStream(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<ReadableStream> GetReadableAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "readable");
        return await ReadableStream.CreateAsync(JSRuntime, jSInstance);
    }

    public async Task<WritableStream> GetWritableAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "writable");
        return await WritableStream.CreateAsync(JSRuntime, jSInstance);
    }
}
