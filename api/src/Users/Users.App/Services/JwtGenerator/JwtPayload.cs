using FileSaver.Adapter.ValueObjects;
using Users.Core.Models;
using Users.Core.Models.UserModel;

namespace Users.App.Services.JwtGenerator;

#nullable enable
public record JwtPayload(User user);
#nullable restore