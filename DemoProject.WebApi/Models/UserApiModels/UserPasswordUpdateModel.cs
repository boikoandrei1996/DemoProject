using System.ComponentModel.DataAnnotations;

namespace DemoProject.WebApi.Models.UserApiModels
{
  public sealed class UserPasswordUpdateModel
  {
    [Required]
    [StringLength(100, MinimumLength = 5)]
    public string OldPassword { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 5)]
    public string NewPassword { get; set; }
  }
}
