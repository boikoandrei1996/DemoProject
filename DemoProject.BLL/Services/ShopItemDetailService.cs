using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.BLL.Services
{
  public class ShopItemDetailService : IShopItemDetailService
  {
    private readonly IDbContext _context;

    public ShopItemDetailService(IDbContext context)
    {
      _context = context;
    }

    public Task<bool> ExistAsync(Expression<Func<ShopItemDetail, bool>> filter)
    {
      Check.NotNull(filter, nameof(filter));

      return _context.ShopItemDetails.AnyAsync(filter);
    }

    public async Task<ServiceResult> AddAsync(ShopItemDetail model)
    {
      Check.NotNull(model, nameof(model));
      Check.Positive(model.Price, nameof(model.Price));

      var shopItemExist = await _context.ShopItems.AnyAsync(x => x.Id == model.ShopItemId);
      if (shopItemExist == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(model.ShopItemId), $"ShopItem not found with id: '{model.ShopItemId}'.");
      }

      _context.ShopItemDetails.Add(model);

      return await _context.SaveAsync(nameof(AddAsync), model);
    }

    public Task<ServiceResult> UpdateAsync(ShopItemDetail model)
    {
      throw new NotImplementedException();
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var model = await _context.ShopItemDetails.FirstOrDefaultAsync(x => x.Id == id);
      if (model == null)
      {
        return ServiceResultFactory.Success;
      }

      _context.ShopItemDetails.Remove(model);

      return await _context.SaveAsync<ShopItemDetail>(nameof(DeleteAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
