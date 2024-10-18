using FileSaver.Adapter.Types;

namespace FileSaver.Adapter;

public interface IFileApi
{
  FileAddress GetAddress(FilePath path);
  FilePath GetPath(FileAddress address);
}