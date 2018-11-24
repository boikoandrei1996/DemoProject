using System;

namespace DemoProject.Shared
{
  public static class Check
  {
    public static void NotNull<T>(T value, string paramName)
    {
      if (ReferenceEquals(value, null))
      {
        throw new ArgumentNullException(paramName);
      }
    }
  }
}
