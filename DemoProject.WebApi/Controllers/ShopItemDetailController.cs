using System;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.Shared;
using DemoProject.WebApi.Attributes;
using DemoProject.WebApi.Models.ShopItemDetailApiModels;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  [Route("api/[controller]")]
  [HandleServiceResult]
  [ValidateModelState]
  public class ShopItemDetailController : Controller
  {
    private readonly IShopItemDetailService _shopItemDetailService;

    public ShopItemDetailController(
      IShopItemDetailService shopItemDetailService)
    {
      _shopItemDetailService = shopItemDetailService;
    }

    // POST api/shopitemdetail
    [HttpPost]
    public Task<ServiceResult> Add([FromBody]ShopItemDetailAddModel apiEntity)
    {
      var entity = ShopItemDetailAddModel.Map(apiEntity);

      return _shopItemDetailService.AddAsync(entity);
    }

    // PUT api/shopitemdetail/{id}
    [HttpPut("{id:guid}")]
    public Task<ServiceResult> Edit(Guid id, [FromBody]ShopItemDetailEditModel apiEntity)
    {
      var entity = ShopItemDetailEditModel.Map(apiEntity, id);

      return _shopItemDetailService.UpdateAsync(entity);
    }

    // DELETE api/shopitemdetail/{id}
    [HttpDelete("{id:guid}")]
    public Task<ServiceResult> Delete(Guid id)
    {
      return _shopItemDetailService.DeleteAsync(id);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        _shopItemDetailService.Dispose();
      }

      base.Dispose(disposing);
    }
  }
}
