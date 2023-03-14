[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](/LICENSE.md)
[![GitHub issues](https://img.shields.io/github/issues/KristofferStrube/Blazor.CompressionStreams)](https://github.com/KristofferStrube/Blazor.CompressionStreams/issues)
[![GitHub forks](https://img.shields.io/github/forks/KristofferStrube/Blazor.CompressionStreams)](https://github.com/KristofferStrube/Blazor.CompressionStreams/network/members)
[![GitHub stars](https://img.shields.io/github/stars/KristofferStrube/Blazor.CompressionStreams)](https://github.com/KristofferStrube/Blazor.CompressionStreams/stargazers)

<!--[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/KristofferStrube.Blazor.CompressionStreams?label=NuGet%20Downloads)](https://www.nuget.org/packages/KristofferStrube.Blazor.CompressionStreams/) -->

# Introduction
A Blazor wrapper for the browser API [Compression Streams](https://wicg.github.io/compression/)

The API defines way to compress and decompress streams of binary data. This project implements a wrapper around the API for Blazor so that we can easily and safely compress and decompress streams.

# Demo
The sample project can be demoed at https://kristofferstrube.github.io/Blazor.CompressionStreams/

On each page you can find the corresponding code for the example in the top right corner.

On the [API Coverage Status page](https://kristofferstrube.github.io/Blazor.CompressionStreams/Status) you can see how much of the WebIDL specs this wrapper has covered.

# Getting Started
The package can be used in Blazor projects.
## Prerequisites
You need to install .NET 7.0 or newer to use the library.

[Download .NET 7](https://dotnet.microsoft.com/download/dotnet/7.0)

## Installation
You can install the package via Nuget with the Package Manager in your IDE or alternatively using the command line:
```bash
dotnet add package KristofferStrube.Blazor.CompressionStreams
```

## Import
You need to reference the package in order to use it in your pages. This can be done in `_Import.razor` by adding the following.
```razor
@using KristofferStrube.Blazor.CompressionStreams
```
## Creating wrapper instance
You can create wrapper instances for `CompressionStream` and `DecompressionStream` with the two following sets of constructors which either take a format or a reference to an existing JS CompressionStream.
```csharp
// Compression Stream constructions
var newCompressionStream = await CompressionStream.CreateAsync(JSRuntime, CompressionAlgorithm.Deflate);

IJSObjectReference jSCompressionStream;
var existingCompressionStream = await CompressionStream.CreateAsync(JSRuntime, jSCompressionStream);

// Decompression Stream constructions
var newDecompressionStream = await DecompressionStream.CreateAsync(JSRuntime, CompressionAlgorithm.Deflate);

IJSObjectReference jSDecompressionStream;
var existingDecompressionStream = await DecompressionStream.CreateAsync(JSRuntime, jSDecompressionStream);
```
## Using `CompressionStream` and `DecompressionStream`
The primary usage of the `CompressionStream`s and `DecompressionStream`s are in combination with the [Blazor.Streams](https://github.com/KristofferStrube/Blazor.Streams) library where the `ReadableStream` can be piped through any stream that implements the `IGenericTransformStream` interface which both `CompressionStream` and `DecompressionStream` does.

```
ReadableStream readableStream;

ReadableStream compressed = await readableStream.PipeThroughAsync(newCompressionStream);

ReadableStream decompressed = await compressed.PipeThroughAsync(newDecompressionStream);
```

This can be useful if you have limited bandwidth and want to stream some content.

# Issues
Feel free to open issues on the repository if you find any errors with the package or have wishes for features.

# Related articles
This repository was build with inspiration and help from the following series of articles:

- [Wrapping JavaScript libraries in Blazor WebAssembly/WASM](https://blog.elmah.io/wrapping-javascript-libraries-in-blazor-webassembly-wasm/)
- [Call anonymous C# functions from JS in Blazor WASM](https://blog.elmah.io/call-anonymous-c-functions-from-js-in-blazor-wasm/)
- [Using JS Object References in Blazor WASM to wrap JS libraries](https://blog.elmah.io/using-js-object-references-in-blazor-wasm-to-wrap-js-libraries/)
- [Blazor WASM 404 error and fix for GitHub Pages](https://blog.elmah.io/blazor-wasm-404-error-and-fix-for-github-pages/)
- [How to fix Blazor WASM base path problems](https://blog.elmah.io/how-to-fix-blazor-wasm-base-path-problems/)
