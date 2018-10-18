using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Interfaces;
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

    // PUT api/order/{id}
    [HttpPut("{id:guid}")]
    public Task<ServiceResult> Edit(Guid id, [FromBody]OrderEditModel apiEntity)
    {
      var entity = OrderEditModel.Map(apiEntity, id);

      return _orderService.UpdateAsync(entity);
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
