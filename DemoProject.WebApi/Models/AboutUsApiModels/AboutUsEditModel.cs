using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DAL.Enums;
using DemoProject.DAL.Models;
using DemoProject.Shared.Attributes;

namespace DemoProject.WebApi.Models.AboutUsApiModels
{
  public class AboutUsEditModel
  {
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MinimumValueValidation]
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
        GroupName = ContentGroupName.AboutUs
      };
    }
  }
}
