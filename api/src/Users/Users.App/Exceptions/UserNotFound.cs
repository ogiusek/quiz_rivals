using Common.Exceptions;

namespace Users.App.Exceptions;

public class UserNotFound : CustomException
{
  public UserNotFound() : base(404, "User with this nick and password does not exist")
  { }
}