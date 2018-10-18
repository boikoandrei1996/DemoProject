using System.Collections.Generic;
using System.Linq;
using DemoProject.DLL.Models.Pages;
using DemoProject.WebApi.Models.OrderApiModels;

namespace DemoProject.WebApi.Models.Pages
{
  public class OrderPageModel
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<OrderViewModel> Records { get; set; } = new List<OrderViewModel>();

    public static OrderPageModel Map(OrderPage model)
    {
      if (model == null)
      {
        return null;
      }

      return new OrderPageModel
      {
        CurrentPage = model.CurrentPage,
        PageSize = model.PageSize,
        TotalPages = model.TotalPages,
        Records = model.Records.Select(OrderViewModel.Map).ToList()
      };
    }
  }
}
