using System;
using System.Collections.Generic;
using System.Linq;
using DemoProject.DAL.Models;
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

      if (model.Items == null)
      {
        model.Items = new List<InfoObject>();
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
