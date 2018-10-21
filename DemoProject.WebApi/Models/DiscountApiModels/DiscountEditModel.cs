using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DLL.Models;
using DemoProject.WebApi.Attributes.ValidationAttributes;

namespace DemoProject.WebApi.Models.DiscountApiModels
{
  public class DiscountEditModel
  {
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MinimumValueValidation]
    public int Order { get; set; }

    public static ContentGroup Map(DiscountEditModel model, Guid id)
    {
      if (model == null)
      {
        return null;
      }

      return new ContentGroup
      {
        Id = id,
        Title = model.Title,
        Order = model.Order,
        GroupName = GroupName.Discount
      };
    }
  }
}
