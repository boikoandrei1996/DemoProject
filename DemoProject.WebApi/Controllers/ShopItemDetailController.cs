using System;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.Shared.Attributes;
using DemoProject.WebApi.Models.ShopItemDetailApiModels;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  [Route("api/[controller]")]
  [HandleServiceResult]
  [ValidateModelState]
  public sealed class ShopItemDetailController : Controller
  {
    private readonly IShopItemDetailService _shopItemDetailService;

    public ShopItemDetailController(
      IShopItemDetailService shopItemDetailService)
    {
      _shopItemDetailService = shopItemDetailService;
    }

    // POST api/shopitemdetail
    [HttpPost]
    public async Task<ServiceResult> Add([FromBody]ShopItemDetailAddModel apiEntity)
    {
      var entity = ShopItemDetailAddModel.Map(apiEntity);

      var result = await _shopItemDetailService.AddAsync(entity);
      if (result.TryCastModel(out ShopItemDetail detail))
      {
        result.ViewModel = ShopItemDetailViewModel.Map(detail);
      }

      return result;
    }

    // PUT api/shopitemdetail/{id}
    [HttpPut("{id:guid}")]
    public async Task<ServiceResult> Edit(Guid id, [FromBody]ShopItemDetailEditModel apiEntity)
    {
      var entity = ShopItemDetailEditModel.Map(apiEntity, id);

      var result = await _shopItemDetailService.UpdateAsync(entity);
      if (result.TryCastModel(out ShopItemDetail detail))
      {
        result.ViewModel = ShopItemDetailViewModel.Map(detail);
      }

      return result;
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
