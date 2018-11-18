using System.Collections.Generic;
using System.Linq;
using DemoProject.DAL.Models.Pages;
using DemoProject.WebApi.Models.ShopItemApiModels;

namespace DemoProject.WebApi.Models.Pages
{
  public class ShopItemPageModel
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<ShopItemViewModel> Records { get; set; } = new List<ShopItemViewModel>();

    public static ShopItemPageModel Map(ShopItemPage model)
    {
      if (model == null)
      {
        return null;
      }

      return new ShopItemPageModel
      {
        CurrentPage = model.CurrentPage,
        PageSize = model.PageSize,
        TotalPages = model.TotalPages,
        Records = model.Records.Select(ShopItemViewModel.Map).ToList()
      };
    }
  }
}
