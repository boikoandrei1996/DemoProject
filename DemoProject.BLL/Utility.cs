using System;

namespace DemoProject.BLL
{
  internal static class Utility
  {
    public static bool IsModified(string oldValue, string newValue, bool ignoreCase = false)
    {
      if (string.IsNullOrEmpty(newValue))
      {
        return false;
      }

      if (ignoreCase)
      {
        if (newValue.Equals(oldValue, StringComparison.OrdinalIgnoreCase))
        {
          return false;
        }
      }
      else
      {
        if (newValue == oldValue)
        {
          return false;
        }
      }

      return true;
    }

    public static bool IsModified(Guid oldValue, Guid newValue)
    {
      if (newValue == default(Guid))
      {
        return false;
      }

      if (newValue == oldValue)
      {
        return false;
      }

      return true;
    }

    public static bool IsModified(decimal oldValue, decimal newValue)
    {
      if (newValue == default(decimal))
      {
        return false;
      }

      if (newValue == oldValue)
      {
        return false;
      }

      return true;
    }

    public static bool IsModified(int oldValue, int newValue)
    {
      if (newValue == default(int))
      {
        return false;
      }

      if (newValue == oldValue)
      {
        return false;
      }

      return true;
    }
  }
}
