using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DLL.Models;
using DemoProject.WebApi.Attributes.ValidationAttributes;

namespace DemoProject.WebApi.Models.ShopItemApiModels
{
  public class ShopItemAddModel
  {
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    public string Description { get; set; }

    [Required]
    [MaxLength(100)]
    [FileExistValidation(Constants.DEFAULT_PATH_TO_IMAGE)]
    public string ImageFilename { get; set; }

    [Required]
    public Guid MenuItemId { get; set; }

    public static ShopItem Map(ShopItemAddModel model)
    {
      if (model == null)
      {
        return null;
      }

      return new ShopItem
      {
        Title = model.Title,
        Description = model.Description,
        ImagePath = Constants.GetRelativePathToImage(model.ImageFilename),
        MenuItemId = model.MenuItemId
      };
    }
  }
}
