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
  public class OrderService : IOrderService
  {
    private readonly IDbContext _context;

    public OrderService(IDbContext context)
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
        return ServiceResultFactory.BadRequestResult(nameof(AddAsync), $"Cart not found with id: '{model.CartId}'.");
      }

      _context.Orders.Add(model);

      var result = await _context.SaveAsync(nameof(AddAsync));
      result.SetModelIfSuccess(model);

      return result;
    }

    public async Task<ServiceResult> UpdateAsync(Order model)
    {
      Check.NotNull(model, nameof(model));

      var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == model.Id);
      if (order == null)
      {
        return ServiceResultFactory.NotFound;
      }

      if (order.DateOfApproved.HasValue || order.DateOfRejected.HasValue || order.DateOfClosed.HasValue)
      {
        return ServiceResultFactory.BadRequestResult(nameof(UpdateAsync), $"Order is already proccessed.");
      }

      var changed = false;

      // update name
      if (Utility.IsModified(order.Name, model.Name))
      {
        order.Name = model.Name;
        changed = true;
      }

      // update mobile
      if (Utility.IsModified(order.Mobile, model.Mobile))
      {
        order.Mobile = model.Mobile;
        changed = true;
      }

      // update address
      if (Utility.IsModified(order.Address, model.Address))
      {
        order.Address = model.Address;
        changed = true;
      }

      // update comment
      if (Utility.IsModified(order.Comment, model.Comment))
      {
        order.Comment = model.Comment;
        changed = true;
      }

      if (changed == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(UpdateAsync), "Nothing to update.");
      }

      _context.Orders.Update(order);

      var result = await _context.SaveAsync(nameof(UpdateAsync));
      result.SetModelIfSuccess(order);

      return result;
    }

    public async Task<ServiceResult> ProccessOrderAsync(ProcessOrderType processOrder, Guid id, Guid userId)
    {
      var userExist = await _context.Users.AnyAsync(x => x.Id == userId);
      if (userExist == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(ProccessOrderAsync), "User not found.");
      }

      var order = await this.FindByAsync(x => x.Id == id);
      if (order == null)
      {
        return ServiceResultFactory.NotFound;
      }

      ServiceResult result;
      switch (processOrder)
      {
        case ProcessOrderType.Approve:
          result = await this.ApproveAsync(order, userId);
          break;
        case ProcessOrderType.Reject:
          result = await this.RejectAsync(order, userId);
          break;
        case ProcessOrderType.Close:
          result = await this.CloseAsync(order, userId);
          break;
        default:
          throw new NotImplementedException(nameof(ProcessOrderType));
      }

      result.SetModelIfSuccess(order);

      return result;
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var model = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
      if (model == null)
      {
        return ServiceResultFactory.NotFound;
      }

      _context.Orders.Remove(model);

      return await _context.SaveAsync(nameof(DeleteAsync));
    }

    private async Task<ServiceResult> ApproveAsync(Order order, Guid userId)
    {
      if (order.DateOfApproved.HasValue)
      {
        return ServiceResultFactory.Success;
      }

      if (order.DateOfRejected.HasValue)
      {
        return ServiceResultFactory.BadRequestResult(nameof(ApproveAsync), "Order is already rejected.");
      }

      if (order.DateOfClosed.HasValue)
      {
        return ServiceResultFactory.BadRequestResult(nameof(ApproveAsync), "Order is already closed.");
      }

      order.DateOfApproved = DateTime.UtcNow;
      order.ApproveUserId = userId;

      _context.Orders.Update(order);

      return await _context.SaveAsync(nameof(ApproveAsync));
    }

    private async Task<ServiceResult> RejectAsync(Order order, Guid userId)
    {
      if (order.DateOfRejected.HasValue)
      {
        return ServiceResultFactory.Success;
      }

      if (order.DateOfApproved.HasValue)
      {
        return ServiceResultFactory.BadRequestResult(nameof(RejectAsync), "Order is already approved.");
      }

      if (order.DateOfClosed.HasValue)
      {
        return ServiceResultFactory.BadRequestResult(nameof(RejectAsync), "Order is already closed.");
      }

      order.DateOfRejected = DateTime.UtcNow;
      order.RejectUserId = userId;

      _context.Orders.Update(order);

      return await _context.SaveAsync(nameof(RejectAsync));
    }

    private async Task<ServiceResult> CloseAsync(Order order, Guid userId)
    {
      if (order.DateOfClosed.HasValue)
      {
        return ServiceResultFactory.Success;
      }
      
      if (order.DateOfApproved.HasValue == false && order.DateOfRejected.HasValue == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(CloseAsync), "Order should be approved or rejected.");
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
