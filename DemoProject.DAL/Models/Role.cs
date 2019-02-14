using System;

namespace DemoProject.DAL.Models
{
  public static class Role
  {
    public const string Admin = "Admin";
    public const string Moderator = "Moderator";
    public const string AdminAndModerator = Role.Admin + "," + Role.Moderator;

    public static string[] GetAllRoles()
    {
      return new[]
      {
        Role.Admin,
        Role.Moderator
      };
    }

    public static string NormalizeRoleName(string role)
    {
      if (string.Equals(role, Role.Admin, StringComparison.OrdinalIgnoreCase))
      {
        return Role.Admin;
      }

      if (string.Equals(role, Role.Moderator, StringComparison.OrdinalIgnoreCase))
      {
        return Role.Moderator;
      }

      return null;
    }
  }
}
