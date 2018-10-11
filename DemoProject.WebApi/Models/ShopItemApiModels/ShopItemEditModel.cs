using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DLL.Models;
using DemoProject.WebApi.Attributes;

namespace DemoProject.WebApi.Models.ShopItemApiModels
{
  public class ShopItemEditModel
  {
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    public string Description { get; set; }

    [Required]
    [MaxLength(100)]
    [ImageExistValidation]
    public string ImageFilename { get; set; }

    public Guid MenuItemId { get; set; }

    public static ShopItem Map(ShopItemEditModel model, Guid id)
    {
      if (model == null)
      {
        return null;
      }

      return new ShopItem
      {
        Id = id,
        Title = model.Title,
        Description = model.Description,
        ImagePath = Constants.DEFAULT_PATH_TO_IMAGE + model.ImageFilename,
        MenuItemId = model.MenuItemId
      };
    }
  }
}
