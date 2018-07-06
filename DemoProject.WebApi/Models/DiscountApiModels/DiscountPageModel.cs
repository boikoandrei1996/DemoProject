using System.Collections.Generic;
using System.Linq;
using DemoProject.DLL.Pages;

namespace DemoProject.WebApi.Models.DiscountApiModels
{
  /// <summary>
  /// Page of discounts.
  /// </summary>
  public class DiscountPageModel
  {
    /// <summary>
    /// Index of current page.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Page size.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total pages count.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Items in current page.
    /// </summary>
    public ICollection<DiscountViewModel> Records { get; set; } = new List<DiscountViewModel>();

    public static DiscountPageModel Map(DiscountPage model)
    {
      if (model == null)
      {
        return null;
      }

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
