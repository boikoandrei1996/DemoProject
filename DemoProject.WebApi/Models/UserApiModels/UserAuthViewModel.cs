using DemoProject.DAL.Models;

namespace DemoProject.WebApi.Models.UserApiModels
{
  public sealed class UserAuthViewModel
  {
    public UserViewModel User { get; set; }
    public string Token { get; set; }

    public static UserAuthViewModel Map(AppUser model, string token)
    {
      return new UserAuthViewModel
      {
        User = UserViewModel.Map(model),
        Token = token
      };
    }
  }
}
