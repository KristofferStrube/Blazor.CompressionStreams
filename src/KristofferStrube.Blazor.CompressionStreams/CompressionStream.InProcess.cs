using KristofferStrube.Blazor.Streams;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.CompressionStreams;

public class CompressionStreamInProcess : CompressionStream, IGenericTransformStreamInProcess
{
    public new IJSInProcessObjectReference JSReference { get; set; }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="CompressionStreamInProcess"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="CompressionStreamInProcess"/>.</param>
    /// <returns>A wrapper instance for a <see cref="CompressionStreamInProcess"/>.</returns>
    public static Task<CompressionStreamInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
        => Task.FromResult(new CompressionStreamInProcess(jSRuntime, jSReference));

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="format">A valid compression format i.e. deflate, deflate-raw or gzip</param>
    /// <returns>A wrapper instance for a <see cref="CompressionStreamInProcess"/>.</returns>
    public new static async Task<CompressionStream> CreateAsync(IJSRuntime jSRuntime, CompressionAlgorithm format)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSInProcessObjectReference jSInstance = await helper.InvokeAsync<IJSInProcessObjectReference>("createCompressionStream", format.AsString());
        return await Task.FromResult(new CompressionStreamInProcess(jSRuntime, jSInstance));
    }

    protected CompressionStreamInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
    {
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
