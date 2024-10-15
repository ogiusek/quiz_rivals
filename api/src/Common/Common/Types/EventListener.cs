namespace Common.Types;

public record EventListener<T>(
  Id Id,
  Func<T, Task> Action
);