using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.BLL.Services
{
  public class ShopItemDetailService : BaseService<ShopItemDetail>, IShopItemDetailService
  {
    public ShopItemDetailService(IDbContext context) : base(context)
    {
    }

    public async Task<ServiceResult> AddAsync(ShopItemDetail model)
    {
      Check.NotNull(model, nameof(model));

      var shopItemExist = await _context.ShopItems.AnyAsync(x => x.Id == model.ShopItemId);
      if (shopItemExist == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(AddAsync), $"ShopItem not found with id: '{model.ShopItemId}'.");
      }

      _context.ShopItemDetails.Add(model);

      var result = await _context.SaveAsync(nameof(AddAsync));
      result.SetModelIfSuccess(model);

      return result;
    }

    public async Task<ServiceResult> UpdateAsync(ShopItemDetail model)
    {
      Check.NotNull(model, nameof(model));

      var shopItemDetail = await _context.ShopItemDetails.FirstOrDefaultAsync(x => x.Id == model.Id);
      if (shopItemDetail == null)
      {
        return ServiceResultFactory.NotFound;
      }

      var changed = false;

      // update shopItem
      if (Utility.IsModified(shopItemDetail.ShopItemId, model.ShopItemId))
      {
        var shopItemExist = await _context.ShopItems.AnyAsync(x => x.Id == model.ShopItemId);
        if (shopItemExist == false)
        {
          return ServiceResultFactory.BadRequestResult(nameof(UpdateAsync), $"ShopItem not found with id: '{model.ShopItemId}'.");
        }

        shopItemDetail.ShopItemId = model.ShopItemId;
        changed = true;
      }

      // update suborder
      if (Utility.IsModified(shopItemDetail.SubOrder, model.SubOrder))
      {
        shopItemDetail.SubOrder = model.SubOrder;
        changed = true;
      }

      // update price
      if (Utility.IsModified(shopItemDetail.Price, model.Price))
      {
        shopItemDetail.Price = model.Price;
        changed = true;
      }

      // update kind
      if (Utility.IsModified(shopItemDetail.Kind, model.Kind))
      {
        shopItemDetail.Kind = model.Kind;
        changed = true;
      }

      // update quantity
      if (Utility.IsModified(shopItemDetail.Quantity, model.Quantity))
      {
        shopItemDetail.Quantity = model.Quantity;
        changed = true;
      }

      if (changed == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(UpdateAsync), "Nothing to update.");
      }

      _context.ShopItemDetails.Update(shopItemDetail);

      var result = await _context.SaveAsync(nameof(UpdateAsync));
      result.SetModelIfSuccess(shopItemDetail);

      return result;
    }
  }
}
