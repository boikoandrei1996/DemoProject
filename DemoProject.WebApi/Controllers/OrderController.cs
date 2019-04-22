using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.BLL;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.Shared.Attributes;
using DemoProject.WebApi.Models.OrderApiModels;
using DemoProject.WebApi.Models.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  [Route("api/[controller]")]
  [HandleServiceResult]
  [ValidateModelState]
  public sealed class OrderController : Controller
  {
    private readonly IOrderService _orderService;

    public OrderController(
      IOrderService orderService)
    {
      _orderService = orderService;
    }

    // GET api/order/all
    [HttpGet("all")]
    public async Task<IEnumerable<OrderViewModel>> GetAll()
    {
      var entities = await _orderService.GetListAsync();

      return entities.Select(OrderViewModel.Map);
    }

    // GET api/order/page/{index}
    [HttpGet("page/{index:int?}")]
    public async Task<PageModel<OrderViewModel>> GetPage(int? index, [FromQuery]int? pageSize)
    {
      var page = await _orderService.GetPageAsync(
        index >= 1 ? index.Value : Constants.DEFAULT_PAGE_INDEX,
        pageSize >= 1 ? pageSize.Value : Constants.DEFAULT_PAGE_SIZE);

      return PageModel<OrderViewModel>.Map(page, OrderViewModel.Map);
    }

    // GET api/order/{id}
    [HttpGet("{id:guid}")]
    public async Task<OrderViewModel> GetOne(Guid id)
    {
      var entity = await _orderService.FindByAsync(x => x.Id == id);

      return OrderViewModel.Map(entity);
    }

    // POST api/order
    [HttpPost]
    public async Task<ServiceResult> Add([FromBody]OrderAddModel apiEntity)
    {
      var entity = OrderAddModel.Map(apiEntity);

      var result = await _orderService.AddAsync(entity);
      if (result.TryCastModel(out Order order))
      {
        result.ViewModel = OrderViewModel.Map(order);
      }

      return result;
    }

    // POST api/order/{id}/approve
    [HttpPost("{id:guid}/approve")]
    public async Task<ServiceResult> ApproveOrder(Guid id)
    {
      var userId = Guid.Empty;

      var result = await _orderService.ProccessOrderAsync(ProcessOrderType.Approve, id, userId);
      if (result.TryCastModel(out Order order))
      {
        result.ViewModel = OrderViewModel.Map(order);
      }

      return result;
    }

    // POST api/order/{id}/reject
    [HttpPost("{id:guid}/reject")]
    public async Task<ServiceResult> RejectOrder(Guid id)
    {
      var userId = Guid.Empty;

      var result = await _orderService.ProccessOrderAsync(ProcessOrderType.Reject, id, userId);
      if (result.TryCastModel(out Order order))
      {
        result.ViewModel = OrderViewModel.Map(order);
      }

      return result;
    }

    // POST api/order/{id}/close
    [HttpPost("{id:guid}/close")]
    public async Task<ServiceResult> CloseOrder(Guid id)
    {
      var userId = Guid.Empty;

      var result = await _orderService.ProccessOrderAsync(ProcessOrderType.Close, id, userId);
      if (result.TryCastModel(out Order order))
      {
        result.ViewModel = OrderViewModel.Map(order);
      }

      return result;
    }

    // DELETE api/order/{id}
    [HttpDelete("{id:guid}")]
    public Task<ServiceResult> Delete(Guid id)
    {
      return _orderService.DeleteAsync(id);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        _orderService.Dispose();
      }

      base.Dispose(disposing);
    }
  }
}
