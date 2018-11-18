﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.DAL.Models
{
  public class ShopItemDetail : BaseEntity
  {
    public int SubOrder { get; set; }

    // Ex. Big, small, medium
    public string Kind { get; set; }

    // Ex. 0.5kg, 1 litr
    public string Quantity { get; set; }

    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    public Guid ShopItemId { get; set; }
    public ShopItem ShopItem { get; set; }

    public ICollection<CartShopItem> CartShopItems { get; set; } = new List<CartShopItem>();
  }
}