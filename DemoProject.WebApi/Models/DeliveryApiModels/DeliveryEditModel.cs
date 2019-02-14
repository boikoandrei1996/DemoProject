using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DAL.Enums;
using DemoProject.DAL.Models;
using DemoProject.Shared.Attributes;

namespace DemoProject.WebApi.Models.DeliveryApiModels
{
  public class DeliveryEditModel
  {
    [Required]
    [MinimumValueValidation]
    public int Order { get; set; }

    public static ContentGroup Map(DeliveryEditModel model, Guid id)
    {
      if (model == null)
      {
        return null;
      }

      return new ContentGroup
      {
        Id = id,
        Order = model.Order,
        GroupName = ContentGroupName.Delivery
      };
    }
  }
}
