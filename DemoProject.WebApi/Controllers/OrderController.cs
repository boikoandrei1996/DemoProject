using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.DLL.Interfaces;
using DemoProject.Shared;
using DemoProject.WebApi.Attributes;
using DemoProject.WebApi.Models.OrderApiModels;
using DemoProject.WebApi.Models.Pages;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  [Route("api/[controller]")]
  [HandleServiceResult]
  [ValidateModelState]
  public class OrderController : Controller
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
    public async Task<OrderPageModel> GetPage(int? index, [FromQuery]int? pageSize)
    {
      var page = await _orderService.GetPageAsync(
        index >= 1 ? index.Value : Constants.DEFAULT_PAGE_INDEX,
        pageSize >= 1 ? pageSize.Value : Constants.DEFAULT_PAGE_SIZE);

      return OrderPageModel.Map(page);
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
    public Task<ServiceResult> Add([FromBody]OrderAddModel apiEntity)
    {
      var entity = OrderAddModel.Map(apiEntity);

      return _orderService.AddAsync(entity);
    }

    // POST api/order/{id}/approve
    [HttpPost("{id:guid}/approve")]
    public Task<ServiceResult> ApproveOrder(Guid id)
    {
      return _orderService.ApproveAsync(id);
    }

    // POST api/order/{id}/reject
    [HttpPost("{id:guid}/reject")]
    public Task<ServiceResult> RejectOrder(Guid id)
    {
      return _orderService.RejectAsync(id);
    }

    // POST api/order/{id}/close
    [HttpPost("{id:guid}/close")]
    public Task<ServiceResult> CloseOrder(Guid id)
    {
      return _orderService.CloseAsync(id);
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
