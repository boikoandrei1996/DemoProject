using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.BLL.Services
{
  public class CartService : ICartService
  {
    private readonly IDbContext _context;

    public CartService(IDbContext context)
    {
      _context = context;
    }

    public async Task<Page<Cart>> GetPageAsync(int pageIndex, int pageSize, Expression<Func<Cart, bool>> filter = null)
    {
      Check.Positive(pageIndex, nameof(pageIndex));
      Check.Positive(pageSize, nameof(pageSize));

      var query = _context.Carts.AsNoTracking();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      var totalCount = await query.CountAsync();

      var page = Page<Cart>.Create(pageSize, pageIndex, totalCount);

      if (totalCount != 0)
      {
        page.Records = await query
          .Skip((page.Current - 1) * page.Size)
          .Take(page.Size)
          .Include(x => x.CartShopItems)
            .ThenInclude(x => x.ShopItemDetail)
            .ThenInclude(x => x.ShopItem)
          .ToListAsync();
      }

      return page;
    }

    public Task<List<Cart>> GetListAsync(Expression<Func<Cart, bool>> filter = null)
    {
      var query = _context.Carts.AsNoTracking();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      return query
        .Include(x => x.CartShopItems)
          .ThenInclude(x => x.ShopItemDetail)
          .ThenInclude(x => x.ShopItem)
        .ToListAsync();
    }

    public Task<Cart> FindByAsync(Expression<Func<Cart, bool>> filter)
    {
      Check.NotNull(filter, nameof(filter));

      return _context.Carts.AsNoTracking()
        .Include(x => x.CartShopItems)
          .ThenInclude(x => x.ShopItemDetail)
          .ThenInclude(x => x.ShopItem)
        .FirstOrDefaultAsync(filter);
    }

    public Task<bool> ExistAsync(Expression<Func<Cart, bool>> filter)
    {
      Check.NotNull(filter, nameof(filter));

      return _context.Carts.AnyAsync(filter);
    }

    public async Task<ServiceResult> AddAsync(Cart model)
    {
      Check.NotNull(model, nameof(model));

      _context.Carts.Add(model);

      var result = await _context.SaveAsync(nameof(AddAsync));
      result.SetModelIfSuccess(model);

      return result;
    }

    public async Task<ServiceResult> AddItemToCartAsync(Guid cartId, Guid shopItemDetailId, int count)
    {
      Check.Positive(count, nameof(count));

      var cartExist = await this.ExistAsync(x => x.Id == cartId);
      if (cartExist == false)
      {
        return ServiceResultFactory.NotFound;
      }

      var shopItemDetail = await _context.ShopItemDetails.FirstOrDefaultAsync(x => x.Id == shopItemDetailId);
      if (shopItemDetail == null)
      {
        return ServiceResultFactory.NotFound;
      }

      var oldCartShopItem = await _context.CartShopItems
        .FirstOrDefaultAsync(x => x.CartId == cartId && x.ShopItemDetailId == shopItemDetailId);

      if (oldCartShopItem == null)
      {
        // add new item
        var newCartShopItem = new CartShopItem
        {
          CartId = cartId,
          ShopItemDetailId = shopItemDetailId,
          Price = shopItemDetail.Price,
          Count = count
        };
        _context.CartShopItems.Add(newCartShopItem);
      }
      else
      {
        // increase count and update price
        oldCartShopItem.Count += count;
        oldCartShopItem.Price = shopItemDetail.Price;
        _context.CartShopItems.Update(oldCartShopItem);
      }

      var result = await _context.SaveAsync(nameof(AddItemToCartAsync));
      var taskModel = this.FindByAsync(x => x.Id == cartId);
      await result.SetModelIfSuccessAsync(taskModel);

      return result;
    }

    public async Task<ServiceResult> RemoveItemFromCartAsync(Guid cartId, Guid shopItemDetailId, bool removeAllItems)
    {
      var cartExist = await this.ExistAsync(x => x.Id == cartId);
      if (cartExist == false)
      {
        return ServiceResultFactory.NotFound;
      }

      var shopItemDetailExist = await _context.ShopItemDetails.AnyAsync(x => x.Id == shopItemDetailId);
      if (shopItemDetailExist == false)
      {
        return ServiceResultFactory.NotFound;
      }

      var oldCartShopItem = await _context.CartShopItems
        .FirstOrDefaultAsync(x => x.CartId == cartId && x.ShopItemDetailId == shopItemDetailId);
      if (oldCartShopItem == null)
      {
        return ServiceResultFactory.NotFound;
      }

      if (removeAllItems || oldCartShopItem.Count <= 1)
      {
        // remove cartshopitem record
        _context.CartShopItems.Remove(oldCartShopItem);
      }
      else
      {
        // reduce count
        oldCartShopItem.Count -= 1;
        _context.CartShopItems.Update(oldCartShopItem);
      }

      var result = await _context.SaveAsync(nameof(RemoveItemFromCartAsync));
      var taskModel = this.FindByAsync(x => x.Id == cartId);
      await result.SetModelIfSuccessAsync(taskModel);

      return result;
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var model = await _context.Carts.FirstOrDefaultAsync(x => x.Id == id);
      if (model == null)
      {
        return ServiceResultFactory.NotFound;
      }

      _context.Carts.Remove(model);

      return await _context.SaveAsync(nameof(DeleteAsync));
    }

    public Task<ServiceResult> UpdateAsync(Cart model)
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
