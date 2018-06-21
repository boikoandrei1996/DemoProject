using System.Collections.Generic;
using System.Linq;
using DemoProject.DLL.Models.Pages;

namespace DemoProject.WebApi.Models.DiscountApiModels
{
  public class DiscountPageModel
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<DiscountViewModel> Records { get; set; } = new List<DiscountViewModel>();

    public static DiscountPageModel Map(DiscountPage model)
    {
      return new DiscountPageModel
      {
        CurrentPage = model.CurrentPage,
        PageSize = model.PageSize,
        TotalPages = model.TotalPages,
        Records = model.Records.Select(x => DiscountViewModel.Map(x)).ToList()
      };
    }
  }
}
