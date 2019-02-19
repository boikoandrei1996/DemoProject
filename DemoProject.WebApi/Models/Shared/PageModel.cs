using System;
using System.Collections.Generic;
using System.Linq;
using DemoProject.Shared;

namespace DemoProject.WebApi.Models.Shared
{
  public sealed class PageModel<TViewModel>
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<TViewModel> Records { get; set; } = new List<TViewModel>();

    public static PageModel<TViewModel> Map<TModel>(Page<TModel> model, Func<TModel, TViewModel> selector)
    {
      if (model == null)
      {
        return null;
      }

      if (model.Records == null)
      {
        model.Records = new List<TModel>();
      }

      return new PageModel<TViewModel>
      {
        CurrentPage = model.Current,
        PageSize = model.Size,
        TotalPages = model.Total,
        Records = model.Records.Select(selector).ToList()
      };
    }
  }
}
