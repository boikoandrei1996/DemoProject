using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DAL.Models;

namespace DemoProject.WebApi.Models.UserApiModels
{
  public sealed class UserEditModel
  {
    [MaxLength(100)]
    public string FirstName { get; set; }

    [MaxLength(100)]
    public string LastName { get; set; }

    [MaxLength(100)]
    public string Email { get; set; }

    [MaxLength(20)]
    public string PhoneNumber { get; set; }

    public static AppUser Map(Guid id, UserEditModel model)
    {
      if (model == null)
      {
        return null;
      }

      return new AppUser
      {
        Id = id,
        FirstName = model.FirstName,
        LastName = model.LastName,
        Email = model.Email,
        PhoneNumber = model.PhoneNumber
      };
    }
  }
}
