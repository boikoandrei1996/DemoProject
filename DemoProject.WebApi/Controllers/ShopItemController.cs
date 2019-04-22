using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.Shared.Attributes;
using DemoProject.WebApi.Models.Shared;
using DemoProject.WebApi.Models.ShopItemApiModels;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  [Route("api/[controller]")]
  [HandleServiceResult]
  [ValidateModelState]
  public sealed class ShopItemController : Controller
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
    public async Task<PageModel<ShopItemViewModel>> GetPage(Guid menuItemId, int? index, [FromQuery]int? pageSize)
    {
      var page = await _shopItemService.GetPageAsync(
        index >= 1 ? index.Value : Constants.DEFAULT_PAGE_INDEX,
        pageSize >= 1 ? pageSize.Value : Constants.DEFAULT_PAGE_SIZE,
        x => x.MenuItemId == menuItemId);

      return PageModel<ShopItemViewModel>.Map(page, ShopItemViewModel.Map);
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
    public async Task<ServiceResult> Add([FromBody]ShopItemAddModel apiEntity)
    {
      var entity = ShopItemAddModel.Map(apiEntity);

      var result = await _shopItemService.AddAsync(entity);
      if (result.TryCastModel(out ShopItem shopItem))
      {
        result.ViewModel = ShopItemViewModel.Map(shopItem);
      }

      return result;
    }

    // PUT api/shopitem/{id}
    [HttpPut("{id:guid}")]
    public async Task<ServiceResult> Edit(Guid id, [FromBody]ShopItemEditModel apiEntity)
    {
      var entity = ShopItemEditModel.Map(apiEntity, id);

      var result = await _shopItemService.UpdateAsync(entity);
      if (result.TryCastModel(out ShopItem shopItem))
      {
        result.ViewModel = ShopItemViewModel.Map(shopItem);
      }

      return result;
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