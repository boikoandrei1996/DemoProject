using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Models.AboutUsApiModels
{
  public class AboutUsEditModel
  {
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    public int Order { get; set; }

    public static ContentGroup Map(AboutUsEditModel model, Guid id)
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
        GroupName = GroupName.AboutUs
      };
    }
  }
}
