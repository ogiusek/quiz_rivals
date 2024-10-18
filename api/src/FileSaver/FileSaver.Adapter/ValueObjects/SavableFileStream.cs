namespace FileSaver.Adapter.ValueObjects;

public record SavableFileStream(Stream Stream, FileExtension Extension);