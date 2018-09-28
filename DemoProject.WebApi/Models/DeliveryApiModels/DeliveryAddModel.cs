﻿using System.ComponentModel.DataAnnotations;
using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Models.DeliveryApiModels
{
  public class DeliveryAddModel
  {
    [Required]
    public int Order { get; set; }

    public static ContentGroup Map(DeliveryAddModel model)
    {
      if (model == null)
      {
        return null;
      }

      return new ContentGroup
      {
        Order = model.Order,
        GroupName = GroupName.Delivery
      };
    }
  }
}
