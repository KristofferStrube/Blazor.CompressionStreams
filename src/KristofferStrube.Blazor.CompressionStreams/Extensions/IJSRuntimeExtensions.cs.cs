﻿using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.CompressionStreams;

internal static class IJSRuntimeExtensions
{
    internal static async Task<IJSObjectReference> GetHelperAsync(this IJSRuntime jSRuntime)
    {
        return await jSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/KristofferStrube.Blazor.CompressionStreams/KristofferStrube.Blazor.CompressionStreams.js");
    }
    internal static async Task<IJSInProcessObjectReference> GetInProcessHelperAsync(this IJSRuntime jSRuntime)
    {
        return await jSRuntime.InvokeAsync<IJSInProcessObjectReference>(
            "import", "./_content/KristofferStrube.Blazor.CompressionStreams/KristofferStrube.Blazor.CompressionStreams.js");
    }
}