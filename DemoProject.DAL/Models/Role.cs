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

    public static string Normalize(string role)
    {
      if (Role.Admin.Equals(role, StringComparison.OrdinalIgnoreCase))
      {
        return Role.Admin;
      }

      if (Role.Moderator.Equals(role, StringComparison.OrdinalIgnoreCase))
      {
        return Role.Moderator;
      }

      return null;
    }
  }
}
