using System.ComponentModel.DataAnnotations;
using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Models.DiscountApiModels
{
  public class DiscountAddModel
  {
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    public int Order { get; set; }

    public static Discount Map(DiscountAddModel model)
    {
      if (model == null)
      {
        return null;
      }

      return new Discount
      {
        Title = model.Title,
        Order = model.Order
      };
    }
  }
}
