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

    public static Discount Map(DiscountEditModel model)
    {
      if (model == null)
      {
        return null;
      }

      return new Discount
      {
        Title = model.Title
      };
    }
  }
}
