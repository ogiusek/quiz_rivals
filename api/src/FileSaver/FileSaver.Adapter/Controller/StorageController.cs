using Common.Api.Abstractions;
using FileSaver.Adapter.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FileSaver.Adapter.Controller;

[AllowAnonymous]
[Route("storage")]
public class StorageController : CustomController
{
  private readonly IFileStorage _fileStorage;

  public StorageController(IFileStorage fileStorage)
  {
    _fileStorage = fileStorage;
  }

  [HttpGet("{*path}")]
  public async Task<ActionResult<Stream>> Get(string path)
  {
    FilePath filePath = new(path);
    SavableFileStream? fileStream = await _fileStorage.Get(filePath);
    if (fileStream is null)
    {
      return NotFound();
    }

    FileContentType contentType = FileContentType.GetContentType(fileStream.Extension);
    string fileName = filePath.Path.Contains('/') ?
      filePath.Path.Substring(filePath.Path.LastIndexOf('/') + 1) :
      filePath.Path;

    return File(fileStream.Stream, contentType.ContentType, fileName);
  }

}