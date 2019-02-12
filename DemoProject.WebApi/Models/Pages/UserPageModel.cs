using System.Collections.Generic;
using System.Linq;
using DemoProject.DAL.Models;
using DemoProject.Shared.Interfaces;
using DemoProject.WebApi.Models.UserApiModels;

namespace DemoProject.WebApi.Models.Pages
{
  public class UserPageModel
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<UserViewModel> Records { get; set; } = new List<UserViewModel>();

    public static UserPageModel Map(IPage<AppUser> model)
    {
      if (model == null)
      {
        return null;
      }

      if (model.Records == null)
      {
        model.Records = new List<AppUser>();
      }

      return new UserPageModel
      {
        CurrentPage = model.CurrentPage,
        PageSize = model.PageSize,
        TotalPages = model.TotalPages,
        Records = model.Records.Select(UserViewModel.Map).ToList()
      };
    }
  }
}
