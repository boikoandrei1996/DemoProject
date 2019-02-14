using System.ComponentModel.DataAnnotations;
using DemoProject.DAL.Enums;
using DemoProject.DAL.Models;
using DemoProject.Shared.Attributes;

namespace DemoProject.WebApi.Models.DeliveryApiModels
{
  public class DeliveryAddModel
  {
    [Required]
    [MinimumValueValidation]
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
        GroupName = ContentGroupName.Delivery
      };
    }
  }
}
