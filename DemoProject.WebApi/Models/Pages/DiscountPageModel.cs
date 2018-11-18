using System.Collections.Generic;
using System.Linq;
using DemoProject.DAL.Models;
using DemoProject.Shared.Interfaces;
using DemoProject.WebApi.Models.DiscountApiModels;

namespace DemoProject.WebApi.Models.Pages
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

    public static DiscountPageModel Map(IPage<ContentGroup> model)
    {
      if (model == null)
      {
        return null;
      }

      if (model.Records == null)
      {
        model.Records = new List<ContentGroup>();
      }

      return new DiscountPageModel
      {
        CurrentPage = model.CurrentPage,
        PageSize = model.PageSize,
        TotalPages = model.TotalPages,
        Records = model.Records.Select(DiscountViewModel.Map).ToList()
      };
    }
  }
}
