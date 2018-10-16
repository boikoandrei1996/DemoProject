using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.DLL.Extensions;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Interfaces;
using DemoProject.DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DLL.Services
{
  public class CartService : ICartService
  {
    private readonly EFContext _context;

    public CartService(EFContext context)
    {
      _context = context;
    }

    public Task<Cart> FindByAsync(Expression<Func<Cart, bool>> filter)
    {
      if (filter == null)
      {
        throw new ArgumentNullException(nameof(filter));
      }

      return _context.Carts.AsNoTracking()
        .Include(x => x.CartShopItems)
        .ThenInclude(x => x.ShopItemDetail)
        .ThenInclude(x => x.ShopItem)
        .FirstOrDefaultAsync(filter);
    }

    public Task<bool> ExistAsync(Expression<Func<Cart, bool>> filter)
    {
      if (filter == null)
      {
        throw new ArgumentNullException(nameof(filter));
      }

      return _context.Carts.AnyAsync(filter);
    }

    public Task<ServiceResult> AddAsync(Cart model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      _context.Carts.Add(model);

      return _context.SaveChangesSafeAsync(nameof(AddAsync), model.Id);
    }

    public Task<ServiceResult> UpdateAsync(Cart model)
    {
      throw new NotImplementedException();
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var model = await _context.Carts.FirstOrDefaultAsync(x => x.Id == id);
      if (model == null)
      {
        return ServiceResultFactory.Success;
      }

      _context.Carts.Remove(model);

      return await _context.SaveChangesSafeAsync(nameof(DeleteAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
