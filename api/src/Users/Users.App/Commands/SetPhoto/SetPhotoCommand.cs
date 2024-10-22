using Common.App.Abstractions;
using Common.Exceptions;
using Common.Extensions;
using Common.Types;
using FileSaver.Adapter.ValueObjects;
using Users.Core.Models;

namespace Users.App.Commands.SetPhoto;

public record SetPhotoCommand(string Id, SavableFileStream Photo) : ICustomCommand
{
  public Res Validate()
  {
    Res res = Res.Success();
    if (!User.PhotoExtensions.Any(Ext => Ext == Photo.Extension.Extension))
    {
      res.Fail(new BadRequestException($"Photo must be one of the following extensions: {string.Join(", ", User.PhotoExtensions)}"));
    }
    return res;
  }
}