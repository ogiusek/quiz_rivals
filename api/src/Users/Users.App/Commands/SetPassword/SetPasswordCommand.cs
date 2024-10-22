using Common.App.Abstractions;

namespace Users.App.Commands.SetPassword;

public record SetPasswordCommand(
  string Id,
  string Password
) : ICustomCommand;