using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DAL.Models;
using DemoProject.WebApi.Attributes.ValidationAttributes;

namespace DemoProject.WebApi.Models.MenuItemApiModels
{
  public class MenuItemEditModel
  {
    [Required]
    [MinimumValueValidation]
    public int Order { get; set; }

    [Required]
    [MaxLength(100)]
    public string Text { get; set; }

    [Required]
    [MaxLength(100)]
    [FileExistValidation(Constants.DEFAULT_PATH_TO_IMAGE)]
    public string IconFilename { get; set; }

    public static MenuItem Map(MenuItemEditModel model, Guid id)
    {
      if (model == null)
      {
        return null;
      }

      return new MenuItem
      {
        Id = id,
        Order = model.Order,
        Text = model.Text,
        IconPath = Constants.GetRelativePathToImage(model.IconFilename)
      };
    }
  }
}
