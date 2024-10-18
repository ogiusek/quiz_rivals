using Common.App.Types;

namespace Common.App.Adapters;

public interface IFileApi
{
  FileAddress GetAddress(FilePath path);
  FilePath GetPath(FileAddress address);
}