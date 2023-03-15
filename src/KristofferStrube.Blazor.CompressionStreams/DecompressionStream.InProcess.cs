using KristofferStrube.Blazor.Streams;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.CompressionStreams;

public class DecompressionStreamInProcess : CompressionStream, IGenericTransformStreamInProcess
{
    public new IJSInProcessObjectReference JSReference { get; set; }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="DecompressionStreamInProcess"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="DecompressionStreamInProcess"/>.</param>
    /// <returns>A wrapper instance for a <see cref="DecompressionStreamInProcess"/>.</returns>
    public static Task<DecompressionStreamInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
        => Task.FromResult(new DecompressionStreamInProcess(jSRuntime, jSReference));

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="format">A valid compression format i.e. deflate, deflate-raw or gzip</param>
    /// <returns>A wrapper instance for a <see cref="DecompressionStreamInProcess"/>.</returns>
    public new static async Task<DecompressionStreamInProcess> CreateAsync(IJSRuntime jSRuntime, CompressionAlgorithm format)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSInProcessObjectReference jSInstance = await helper.InvokeAsync<IJSInProcessObjectReference>("createDecompressionStream", format.AsString());
        return await Task.FromResult(new DecompressionStreamInProcess(jSRuntime, jSInstance));
    }

    protected DecompressionStreamInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference) {
        JSReference = jSReference;
    }

    public new async Task<ReadableStreamInProcess> GetReadableAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSInProcessObjectReference jSInstance = await helper.InvokeAsync<IJSInProcessObjectReference>("getAttribute", JSReference, "readable");
        return await ReadableStreamInProcess.CreateAsync(JSRuntime, jSInstance);
    }

    public new async Task<WritableStreamInProcess> GetWritableAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSInProcessObjectReference jSInstance = await helper.InvokeAsync<IJSInProcessObjectReference>("getAttribute", JSReference, "writable");
        return await WritableStreamInProcess.CreateAsync(JSRuntime, jSInstance);
    }
}
