using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Interfaces;
using DemoProject.WebApi.Attributes;
using DemoProject.WebApi.Models.Pages;
using DemoProject.WebApi.Models.ShopItemApiModels;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  [Route("api/[controller]")]
  [HandleServiceResult]
  [ValidateModelState]
  public class ShopItemController : Controller
  {
    private readonly IShopItemService _shopItemService;

    public ShopItemController(
      IShopItemService shopItemService)
    {
      _shopItemService = shopItemService;
    }

    // GET api/{menuItemId}/shopitem/all
    [HttpGet("/api/{menuItemId:guid}/shopitem/all")]
    public async Task<IEnumerable<ShopItemViewModel>> GetAll(Guid menuItemId)
    {
      var entities = await _shopItemService.GetListAsync(x => x.MenuItemId == menuItemId);

      return entities.Select(ShopItemViewModel.Map);
    }

    // GET api/{menuItemId}/shopitem/page/{index}
    [HttpGet("/api/{menuItemId:guid}/shopitem/page/{index:int?}")]
    public async Task<ShopItemPageModel> GetPage(Guid menuItemId, int? index, [FromQuery]int? pageSize)
    {
      var page = await _shopItemService.GetPageAsync(
        index >= 1 ? index.Value : Constants.DEFAULT_PAGE_INDEX,
        pageSize >= 1 ? pageSize.Value : Constants.DEFAULT_PAGE_SIZE,
        x => x.MenuItemId == menuItemId);

      return ShopItemPageModel.Map(page);
    }

    // GET api/shopitem/{id}
    [HttpGet("{id:guid}")]
    public async Task<ShopItemViewModel> GetOne(Guid id)
    {
      var entity = await _shopItemService.FindByAsync(x => x.Id == id);

      return ShopItemViewModel.Map(entity);
    }

    // POST api/shopitem
    [HttpPost]
    public Task<ServiceResult> Add([FromBody]ShopItemAddModel apiEntity)
    {
      var entity = ShopItemAddModel.Map(apiEntity);

      return _shopItemService.AddAsync(entity);
    }

    // PUT api/shopitem/{id}
    [HttpPut("{id:guid}")]
    public Task<ServiceResult> Edit(Guid id, [FromBody]ShopItemEditModel apiEntity)
    {
      var entity = ShopItemEditModel.Map(apiEntity, id);

      return _shopItemService.UpdateAsync(entity);
    }

    // DELETE api/shopitem/{id}
    [HttpDelete("{id:guid}")]
    public Task<ServiceResult> Delete(Guid id)
    {
      return _shopItemService.DeleteAsync(id);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        _shopItemService.Dispose();
      }

      base.Dispose(disposing);
    }
  }
}