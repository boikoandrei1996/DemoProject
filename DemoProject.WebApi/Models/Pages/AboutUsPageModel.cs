using System.Collections.Generic;
using System.Linq;
using DemoProject.BLL.PageModels;
using DemoProject.WebApi.Models.AboutUsApiModels;

namespace DemoProject.WebApi.Models.Pages
{
  public class AboutUsPageModel
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<AboutUsViewModel> Records { get; set; } = new List<AboutUsViewModel>();

    public static AboutUsPageModel Map(ContentGroupPage model)
    {
      if (model == null)
      {
        return null;
      }

      return new AboutUsPageModel
      {
        CurrentPage = model.CurrentPage,
        PageSize = model.PageSize,
        TotalPages = model.TotalPages,
        Records = model.Records.Select(AboutUsViewModel.Map).ToList()
      };
    }
  }
}
