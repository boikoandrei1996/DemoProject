using System.Collections.Generic;
using System.Linq;
using DemoProject.DAL.Models;
using DemoProject.Shared.Interfaces;
using DemoProject.WebApi.Models.ShopItemApiModels;

namespace DemoProject.WebApi.Models.Pages
{
  public class ShopItemPageModel
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<ShopItemViewModel> Records { get; set; } = new List<ShopItemViewModel>();

    public static ShopItemPageModel Map(IPage<ShopItem> model)
    {
      if (model == null)
      {
        return null;
      }

      if (model.Records == null)
      {
        model.Records = new List<ShopItem>();
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
