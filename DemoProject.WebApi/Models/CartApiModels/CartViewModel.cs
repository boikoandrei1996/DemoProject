﻿using System;
using System.Collections.Generic;
using System.Linq;
using DemoProject.DAL.Models;
using DemoProject.WebApi.Models.CartShopItemApiModels;

namespace DemoProject.WebApi.Models.CartApiModels
{
  public class CartViewModel
  {
    public Guid Id { get; set; }
    public ICollection<CartShopItemViewModel> Items { get; set; } = new List<CartShopItemViewModel>();
    public int TotalCount { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime DateOfCreation { get; set; }

    public static CartViewModel Map(Cart model)
    {
      if (model == null)
      {
        return null;
      }

      if (model.CartShopItems == null)
      {
        model.CartShopItems = new List<CartShopItem>();
      }

      return new CartViewModel
      {
        Id = model.Id,
        Items = model.CartShopItems.Select(CartShopItemViewModel.Map).ToList(),
        TotalCount = model.CartShopItems.Sum(x => x.Count),
        TotalPrice = model.CartShopItems.Sum(x => x.Price * x.Count),
        DateOfCreation = model.DateOfCreation
      };
    }
  }
}
