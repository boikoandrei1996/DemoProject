using System;
using System.Collections.Generic;
using System.Linq;
using DemoProject.DLL.Models;
using DemoProject.WebApi.Models.InfoObjectApiModels;

namespace DemoProject.WebApi.Models.DeliveryApiModels
{
  public class DeliveryViewModel
  {
    public Guid Id { get; set; }
    public int Order { get; set; }
    public ICollection<InfoObjectViewModel> Items { get; set; } = new List<InfoObjectViewModel>();

    public static DeliveryViewModel Map(ContentGroup model)
    {
      if (model == null)
      {
        return null;
      }

      return new DeliveryViewModel
      {
        Id = model.Id,
        Order = model.Order,
        Items = model.Items.Select(x => InfoObjectViewModel.Map(x)).ToList()
      };
    }
  }
}
