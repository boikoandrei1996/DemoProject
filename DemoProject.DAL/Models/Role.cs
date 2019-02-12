using System;
using System.Collections.Generic;

namespace DemoProject.DAL.Models
{
  public static class Role
  {
    public const string Admin = "Admin";
    public const string Moderator = "Moderator";

    public static IReadOnlyList<string> GetAllRoles()
    {
      return new List<string>
      {
        Role.Admin,
        Role.Moderator
      };
    }

    public static string GetRoleOrNull(string role)
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
