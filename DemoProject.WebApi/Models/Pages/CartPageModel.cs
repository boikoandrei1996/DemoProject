using System.Collections.Generic;
using System.Linq;
using DemoProject.DAL.Models;
using DemoProject.Shared.Interfaces;
using DemoProject.WebApi.Models.CartApiModels;

namespace DemoProject.WebApi.Models.Pages
{
  public class CartPageModel
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<CartViewModel> Records { get; set; } = new List<CartViewModel>();

    public static CartPageModel Map(IPage<Cart> model)
    {
      if (model == null)
      {
        return null;
      }

      return new CartPageModel
      {
        CurrentPage = model.CurrentPage,
        PageSize = model.PageSize,
        TotalPages = model.TotalPages,
        Records = model.Records.Select(CartViewModel.Map).ToList()
      };
    }
  }
}
