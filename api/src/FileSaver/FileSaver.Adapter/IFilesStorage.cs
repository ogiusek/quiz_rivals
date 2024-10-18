using Common.Types;
using FileSaver.Adapter.ValueObjects;
using FileSaver.Adapter.Exceptions;

namespace FileSaver.Adapter;

public interface IFileStorage
{
  /// <exception cref="UnsuportedFileExtensionException"></exception>
  Task<Res<FilePath>> Save(SavableFileStream stream);

  Task<SavableFileStream?> Get(FilePath path);

  /// <exception cref="FileNotFoundException"></exception>
  Task<Res> Remove(FilePath path);
}
