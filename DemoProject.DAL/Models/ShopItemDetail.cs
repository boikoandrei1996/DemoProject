﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DemoProject.Shared.Models;

namespace DemoProject.DAL.Models
{
  public sealed class ShopItemDetail : BaseEntity<Guid>
  {
    public int SubOrder { get; set; }

    /// <summary>
    /// Big, small, medium and etc.
    /// </summary>
    public string Kind { get; set; }

    /// <summary>
    /// 0.5kg, 1 litr and etc.
    /// </summary>
    public string Quantity { get; set; }

    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    public Guid ShopItemId { get; set; }

    public ShopItem ShopItem { get; set; }

    public ICollection<CartShopItem> CartShopItems { get; set; } = new List<CartShopItem>();
  }
}
