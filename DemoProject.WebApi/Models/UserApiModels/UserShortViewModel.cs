using System;
using DemoProject.DAL.Models;

namespace DemoProject.WebApi.Models.UserApiModels
{
  public class UserShortViewModel
  {
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public static UserShortViewModel Map(AppUser model)
    {
      if (model == null)
      {
        return null;
      }

      return new UserShortViewModel
      {
        Id = model.Id,
        Username = model.Username,
        FirstName = model.FirstName,
        LastName = model.LastName,
        Email = model.Email,
        PhoneNumber = model.PhoneNumber
      };
    }
  }
}
