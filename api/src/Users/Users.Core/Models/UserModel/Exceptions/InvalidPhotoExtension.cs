using Common.Core.Exceptions;

namespace Users.Core.Models.UserModel.Exceptions;

public sealed class InvalidPhotoExtension : CoreException
{
  public InvalidPhotoExtension(string extension) : base($"Invalid photo extension .{extension}")
  {
  }
}