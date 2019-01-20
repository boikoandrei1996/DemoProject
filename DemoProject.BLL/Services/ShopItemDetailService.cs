using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.BLL.Services
{
  public class ShopItemDetailService : IShopItemDetailService
  {
    private readonly EFContext _context;

    public ShopItemDetailService(EFContext context)
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

      if (await _context.ShopItems.AnyAsync(x => x.Id == model.ShopItemId) == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(model.ShopItemId), this.GetErrorMessage(model.ShopItemId));
      }

      _context.ShopItemDetails.Add(model);

      return await _context.SaveAsync(nameof(AddAsync), model.Id);
    }

    public async Task<ServiceResult> UpdateAsync(ShopItemDetail model)
    {
      Check.NotNull(model, nameof(model));

      if (await _context.ShopItemDetails.AnyAsync(x => x.Id == model.Id) == false)
      {
        return ServiceResultFactory.NotFound;
      }
      else if (await _context.ShopItems.AnyAsync(x => x.Id == model.ShopItemId) == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(model.ShopItemId), this.GetErrorMessage(model.ShopItemId));
      }

      _context.ShopItemDetails.Update(model);

      return await _context.SaveAsync(nameof(UpdateAsync));
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var model = await _context.ShopItemDetails.FirstOrDefaultAsync(x => x.Id == id);
      if (model == null)
      {
        return ServiceResultFactory.Success;
      }

      _context.ShopItemDetails.Remove(model);

      return await _context.SaveAsync(nameof(DeleteAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }

    private string GetErrorMessage(Guid id)
    {
      return $"ShopItem not found with id: '{id}'.";
    }
  }
}
