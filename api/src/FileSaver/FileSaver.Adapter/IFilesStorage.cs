using Common.Types;
using FileSaver.Adapter.Types;

namespace FileSaver.Adapter;

public interface IFileStorage
{
  // Exceptions:
  //   UnsuportedFileExtensionException
  Task<Res<FilePath>> Save(SavableFileStream stream);

  Task<SavableFileStream?> Get(FilePath path);

  // Exceptions:
  //   FileNotFoundException
  Task<Res> Remove(FilePath path);
}
