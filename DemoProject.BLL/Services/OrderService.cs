using System;
using System.Collections.Generic;
using System.Linq;
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
  public class OrderService : IOrderService
  {
    private readonly EFContext _context;

    public OrderService(EFContext context)
    {
      _context = context;
    }

    public async Task<Page<Order>> GetPageAsync(int pageIndex, int pageSize, Expression<Func<Order, bool>> filter = null)
    {
      Check.Positive(pageIndex, nameof(pageIndex));
      Check.Positive(pageSize, nameof(pageSize));

      var query = _context.Orders.AsNoTracking();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      var totalCount = await query.CountAsync();

      var page = Page<Order>.Create(pageSize, pageIndex, totalCount);

      if (totalCount != 0)
      {
        page.Records = await query
          .Skip((page.Current - 1) * page.Size)
          .Take(page.Size)
          .Include(x => x.Cart)
            .ThenInclude(x => x.CartShopItems)
            .ThenInclude(x => x.ShopItemDetail)
            .ThenInclude(x => x.ShopItem)
          .Include(x => x.ApproveUser)
          .Include(x => x.RejectUser)
          .Include(x => x.CloseUser)
          .ToListAsync();
      }

      return page;
    }

    public Task<List<Order>> GetListAsync(Expression<Func<Order, bool>> filter = null)
    {
      var query = _context.Orders.AsNoTracking();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      return query
        .Include(x => x.Cart)
          .ThenInclude(x => x.CartShopItems)
          .ThenInclude(x => x.ShopItemDetail)
          .ThenInclude(x => x.ShopItem)
        .Include(x => x.ApproveUser)
        .Include(x => x.RejectUser)
        .Include(x => x.CloseUser)
        .ToListAsync();
    }

    public Task<Order> FindByAsync(Expression<Func<Order, bool>> filter)
    {
      Check.NotNull(filter, nameof(filter));

      return _context.Orders.AsNoTracking()
        .Include(x => x.Cart)
          .ThenInclude(x => x.CartShopItems)
          .ThenInclude(x => x.ShopItemDetail)
          .ThenInclude(x => x.ShopItem)
        .Include(x => x.ApproveUser)
        .Include(x => x.RejectUser)
        .Include(x => x.CloseUser)
        .FirstOrDefaultAsync(filter);
    }

    public Task<bool> ExistAsync(Expression<Func<Order, bool>> filter)
    {
      Check.NotNull(filter, nameof(filter));

      return _context.Orders.AnyAsync(filter);
    }

    public async Task<ServiceResult> AddAsync(Order model)
    {
      Check.NotNull(model, nameof(model));

      var cartExist = await _context.Carts.AnyAsync(x => x.Id == model.CartId);
      if (cartExist == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(model.CartId), $"Cart not found with id: '{model.CartId}'.");
      }

      _context.Orders.Add(model);

      return await _context.SaveAsync(nameof(AddAsync), model.Id);
    }

    public Task<ServiceResult> UpdateAsync(Order model)
    {
      throw new NotImplementedException();
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var model = await this.FindByAsync(x => x.Id == id);
      if (model == null)
      {
        return ServiceResultFactory.Success;
      }

      _context.Orders.Remove(model);

      return await _context.SaveAsync(nameof(DeleteAsync));
    }

    public async Task<ServiceResult> ApproveAsync(Guid id, Guid userId)
    {
      var order = await this.FindByAsync(x => x.Id == id);
      if (order == null)
      {
        return ServiceResultFactory.NotFound;
      }

      if (order.DateOfApproved.HasValue)
      {
        return ServiceResultFactory.BadRequestResult(nameof(ApproveAsync), "Order is already approved.");
      }

      if (order.DateOfRejected.HasValue)
      {
        return ServiceResultFactory.BadRequestResult(nameof(ApproveAsync), "Order is already rejected.");
      }

      if (order.DateOfClosed.HasValue)
      {
        return ServiceResultFactory.BadRequestResult(nameof(ApproveAsync), "Order is already closed.");
      }

      var userExist = await _context.Users.AnyAsync(x => x.Id == userId);
      if (userExist == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(ApproveAsync), "User not found.");
      }

      order.DateOfApproved = DateTime.UtcNow;
      order.ApproveUserId = userId;

      _context.Orders.Update(order);

      return await _context.SaveAsync(nameof(ApproveAsync));
    }

    public async Task<ServiceResult> RejectAsync(Guid id, Guid userId)
    {
      var order = await this.FindByAsync(x => x.Id == id);
      if (order == null)
      {
        return ServiceResultFactory.NotFound;
      }

      if (order.DateOfApproved.HasValue)
      {
        return ServiceResultFactory.BadRequestResult(nameof(RejectAsync), "Order is already approved.");
      }

      if (order.DateOfRejected.HasValue)
      {
        return ServiceResultFactory.BadRequestResult(nameof(RejectAsync), "Order is already rejected.");
      }

      if (order.DateOfClosed.HasValue)
      {
        return ServiceResultFactory.BadRequestResult(nameof(RejectAsync), "Order is already closed.");
      }

      var userExist = await _context.Users.AnyAsync(x => x.Id == userId);
      if (userExist == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(RejectAsync), "User not found.");
      }

      order.DateOfRejected = DateTime.UtcNow;
      order.RejectUserId = userId;

      _context.Orders.Update(order);

      return await _context.SaveAsync(nameof(RejectAsync));
    }

    public async Task<ServiceResult> CloseAsync(Guid id, Guid userId)
    {
      var order = await this.FindByAsync(x => x.Id == id);
      if (order == null)
      {
        return ServiceResultFactory.NotFound;
      }

      if (order.DateOfClosed.HasValue)
      {
        return ServiceResultFactory.BadRequestResult(nameof(CloseAsync), "Order is already closed.");
      }
      
      if (order.DateOfApproved.HasValue == false && order.DateOfRejected.HasValue == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(CloseAsync), "Order should be approved or rejected.");
      }

      var userExist = await _context.Users.AnyAsync(x => x.Id == userId);
      if (userExist == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(CloseAsync), "User not found.");
      }

      order.DateOfClosed = DateTime.UtcNow;
      order.CloseUserId = userId;

      _context.Orders.Update(order);

      return await _context.SaveAsync(nameof(CloseAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
