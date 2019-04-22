using System.ComponentModel.DataAnnotations;

namespace DemoProject.WebApi.Models.UserApiModels
{
  public sealed class UserRoleUpdateModel
  {
    [Required]
    [StringLength(20, MinimumLength = 5)]
    public string NewRole { get; set; }
  }
}
