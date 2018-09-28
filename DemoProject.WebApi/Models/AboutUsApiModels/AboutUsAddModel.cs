using System.ComponentModel.DataAnnotations;
using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Models.AboutUsApiModels
{
  public class AboutUsAddModel
  {
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    public int Order { get; set; }

    public static ContentGroup Map(AboutUsAddModel model)
    {
      if (model == null)
      {
        return null;
      }

      return new ContentGroup
      {
        Title = model.Title,
        Order = model.Order,
        GroupName = GroupName.AboutUs
      };
    }
  }
}
