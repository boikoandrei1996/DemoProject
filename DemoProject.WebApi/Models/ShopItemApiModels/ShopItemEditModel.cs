using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DAL.Models;
using DemoProject.WebApi.Attributes.ValidationAttributes;

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
    [FileExistValidation(Constants.DEFAULT_PATH_TO_IMAGE)]
    public string ImageFilename { get; set; }

    [Required]
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
        ImagePath = Constants.GetRelativePathToImage(model.ImageFilename),
        MenuItemId = model.MenuItemId
      };
    }
  }
}
