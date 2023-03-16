export function getAttribute(object, attribute) {
    return object[attribute];
}

export function createCompressionStream(format) {
    return new CompressionStream(format);
}

export function createDecompressionStream(format) {
    return new DecompressionStream(format);
}