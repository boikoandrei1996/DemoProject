using System;
using System.Collections.Generic;
using System.Linq;
using DemoProject.DAL.Models;
using DemoProject.WebApi.Models.ShopItemDetailApiModels;

namespace DemoProject.WebApi.Models.ShopItemApiModels
{
  public class ShopItemViewModel
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public ICollection<ShopItemDetailViewModel> Details { get; set; } = new List<ShopItemDetailViewModel>();

    public static ShopItemViewModel Map(ShopItem model)
    {
      if (model == null)
      {
        return null;
      }

      if (model.Details == null)
      {
        model.Details = new List<ShopItemDetail>();
      }

      return new ShopItemViewModel
      {
        Id = model.Id,
        Title = model.Title,
        Description = model.Description,
        ImagePath = Constants.GetFullPathToImage(model.ImagePath),
        Details = model.Details.Select(x => ShopItemDetailViewModel.Map(x)).ToList()
      };
    }
  }
}
