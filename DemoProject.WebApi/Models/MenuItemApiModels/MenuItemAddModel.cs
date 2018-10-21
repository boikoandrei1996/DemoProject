using System.ComponentModel.DataAnnotations;
using DemoProject.DLL.Models;
using DemoProject.WebApi.Attributes.ValidationAttributes;

namespace DemoProject.WebApi.Models.MenuItemApiModels
{
  public class MenuItemAddModel
  {
    [Required]
    [MinimumValueValidation]
    public int Order { get; set; }

    [Required]
    [MaxLength(100)]
    public string Text { get; set; }

    [Required]
    [MaxLength(100)]
    [ImageExistValidation(Constants.DEFAULT_PATH_TO_IMAGE)]
    public string IconFilename { get; set; }

    public static MenuItem Map(MenuItemAddModel model)
    {
      if (model == null)
      {
        return null;
      }

      return new MenuItem
      {
        Order = model.Order,
        Text = model.Text,
        IconPath = Constants.DEFAULT_PATH_TO_IMAGE + model.IconFilename
      };
    }
  }
}
