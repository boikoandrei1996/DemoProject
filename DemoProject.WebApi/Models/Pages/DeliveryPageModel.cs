using System.Collections.Generic;
using System.Linq;
using DemoProject.DLL.Models.Pages;
using DemoProject.WebApi.Models.DeliveryApiModels;

namespace DemoProject.WebApi.Models.Pages
{
  public class DeliveryPageModel
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<DeliveryViewModel> Records { get; set; } = new List<DeliveryViewModel>();

    public static DeliveryPageModel Map(ContentGroupPage model)
    {
      if (model == null)
      {
        return null;
      }

      return new DeliveryPageModel
      {
        CurrentPage = model.CurrentPage,
        PageSize = model.PageSize,
        TotalPages = model.TotalPages,
        Records = model.Records.Select(x => DeliveryViewModel.Map(x)).ToList()
      };
    }
  }
}
