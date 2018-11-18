using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL;
using DemoProject.DAL.Extensions;
using DemoProject.DAL.Models;
using DemoProject.DAL.Models.Pages;
using DemoProject.Shared;
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

    public async Task<OrderPage> GetPageAsync(int pageIndex, int pageSize, Expression<Func<Order, bool>> filter = null)
    {
      var query = _context.Orders.AsNoTracking();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      var totalCount = await query.CountAsync();

      var page = new OrderPage
      {
        CurrentPage = pageIndex,
        PageSize = pageSize,
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
      };

      page.Records = await query
        .Skip((pageIndex - 1) * pageSize)
        .Take(pageSize)
        .Include(x => x.Cart)
        .ThenInclude(x => x.CartShopItems)
        .ThenInclude(x => x.ShopItemDetail)
        .ThenInclude(x => x.ShopItem)
        .ToListAsync();

      return page;
    }

    public async Task<List<Order>> GetListAsync(Expression<Func<Order, bool>> filter = null)
    {
      var query = _context.Orders.AsNoTracking();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      return await query
        .Include(x => x.Cart)
        .ThenInclude(x => x.CartShopItems)
        .ThenInclude(x => x.ShopItemDetail)
        .ThenInclude(x => x.ShopItem)
        .ToListAsync();
    }

    public Task<Order> FindByAsync(Expression<Func<Order, bool>> filter)
    {
      if (filter == null)
      {
        throw new ArgumentNullException(nameof(filter));
      }

      return _context.Orders.AsNoTracking()
        .Include(x => x.Cart)
        .ThenInclude(x => x.CartShopItems)
        .ThenInclude(x => x.ShopItemDetail)
        .ThenInclude(x => x.ShopItem)
        .FirstOrDefaultAsync(filter);
    }

    public Task<bool> ExistAsync(Expression<Func<Order, bool>> filter)
    {
      if (filter == null)
      {
        throw new ArgumentNullException(nameof(filter));
      }

      return _context.Orders.AnyAsync(filter);
    }

    public async Task<ServiceResult> AddAsync(Order model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      if (await _context.Carts.AnyAsync(x => x.Id == model.CartId) == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(model.CartId), $"Cart not found with id: '{model.CartId}'.");
      }

      _context.Orders.Add(model);

      return await _context.SaveAsync(nameof(AddAsync), model.Id);
    }

    public Task<ServiceResult> UpdateAsync(Order model)
    {
      throw new NotImplementedException();
      //if (model == null)
      //{
      //  throw new ArgumentNullException(nameof(model));
      //}

      //var oldOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == model.Id);
      //if (oldOrder == null)
      //{
      //  return ServiceResultFactory.NotFound;
      //}
      //else
      //{
      //  model.CartId = oldOrder.CartId;
      //}

      //_context.Orders.Update(model);

      //return await _context.SaveChangesSafeAsync(nameof(UpdateAsync));
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var model = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
      if (model == null)
      {
        return ServiceResultFactory.Success;
      }

      _context.Orders.Remove(model);

      return await _context.SaveAsync(nameof(DeleteAsync));
    }

    public async Task<ServiceResult> ApproveAsync(Guid id)
    {
      var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
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

      order.DateOfApproved = DateTime.UtcNow;

      _context.Orders.Update(order);

      return await _context.SaveAsync(nameof(ApproveAsync));
    }

    public async Task<ServiceResult> RejectAsync(Guid id)
    {
      var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
      if (order == null)
      {
        return ServiceResultFactory.NotFound;
      }

      if (order.DateOfRejected.HasValue)
      {
        return ServiceResultFactory.BadRequestResult(nameof(RejectAsync), "Order is already rejected.");
      }

      if (order.DateOfApproved.HasValue)
      {
        return ServiceResultFactory.BadRequestResult(nameof(RejectAsync), "Order is already approved.");
      }

      order.DateOfRejected = DateTime.UtcNow;

      _context.Orders.Update(order);

      return await _context.SaveAsync(nameof(RejectAsync));
    }

    public async Task<ServiceResult> CloseAsync(Guid id)
    {
      var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
      if (order == null)
      {
        return ServiceResultFactory.NotFound;
      }

      if (order.DateOfClosed.HasValue)
      {
        return ServiceResultFactory.BadRequestResult(nameof(CloseAsync), "Order is already closed.");
      }

      if (!order.DateOfApproved.HasValue && !order.DateOfRejected.HasValue)
      {
        return ServiceResultFactory.BadRequestResult(nameof(CloseAsync), "Order should be approved or rejected.");
      }

      order.DateOfClosed = DateTime.UtcNow;

      _context.Orders.Update(order);

      return await _context.SaveAsync(nameof(CloseAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
