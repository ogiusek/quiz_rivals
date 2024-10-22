using Common.Exceptions;

namespace Users.App.Exceptions;

public class NickIsAlreadyTaken : CustomException
{
  public NickIsAlreadyTaken() : base(409, "Nick is already taken")
  { }
}