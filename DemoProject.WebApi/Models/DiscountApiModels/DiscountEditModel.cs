using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Models.DiscountApiModels
{
  public class DiscountEditModel
  {
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    public int Order { get; set; }

    public static Discount Map(DiscountEditModel model, Guid id)
    {
      if (model == null)
      {
        return null;
      }

      return new Discount
      {
        Id = id,
        Title = model.Title,
        Order = model.Order
      };
    }
  }
}
