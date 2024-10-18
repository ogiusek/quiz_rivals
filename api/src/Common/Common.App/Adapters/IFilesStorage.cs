using Common.App.Types;
using Common.Types;

namespace Common.App.Adapters;

public interface IFileStorage
{
  Task<FilePath> SaveFile(SavableFileStream stream);
  Task<Res> RemoveFile(FilePath path);
}
