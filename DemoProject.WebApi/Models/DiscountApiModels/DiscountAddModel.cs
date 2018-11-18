using System.ComponentModel.DataAnnotations;
using DemoProject.DAL.Models;
using DemoProject.WebApi.Attributes.ValidationAttributes;

namespace DemoProject.WebApi.Models.DiscountApiModels
{
  public class DiscountAddModel
  {
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MinimumValueValidation]
    public int Order { get; set; }

    public static ContentGroup Map(DiscountAddModel model)
    {
      if (model == null)
      {
        return null;
      }

      return new ContentGroup
      {
        Title = model.Title,
        Order = model.Order,
        GroupName = GroupName.Discount
      };
    }
  }
}
