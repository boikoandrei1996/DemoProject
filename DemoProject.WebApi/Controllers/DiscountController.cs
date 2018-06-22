using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.DLL.Interfaces;
using DemoProject.DLL.Models;
using DemoProject.WebApi.Models.DiscountApiModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  [Route("api/[controller]")]
  public class DiscountController : Controller
  {
    private readonly IDiscountService _discountService;

    public DiscountController(IDiscountService discountService)
    {
      _discountService = discountService;
    }

    // GET api/discount/all
    [HttpGet("all")]
    public async Task<IEnumerable<DiscountViewModel>> GetAll()
    {
      var entities = await _discountService.GetDiscountsAsync();

      return entities.Select(x => DiscountViewModel.Map(x));
    }

    // GET api/discount/page/{index}
    [HttpGet("page/{index:int?}")]
    public async Task<DiscountPageModel> GetPage(int? index)
    {
      var page = await _discountService.GetPageDiscountsAsync(index ?? 1, 2);

      return DiscountPageModel.Map(page);
    }

    // GET api/discount/{id}
    [HttpGet("{id:guid}")]
    public async Task<DiscountViewModel> GetOne(Guid id)
    {
      var entity = await _discountService.FindByAsync(x => x.Id == id);

      return DiscountViewModel.Map(entity);
    }

    // POST api/discount/add
    [HttpPost("add")]
    public Task<IdentityResult> Add([FromBody]DiscountAddModel apiEntity)
    {
      var entity = DiscountAddModel.Map(apiEntity);

      return _discountService.AddAsync(entity);
    }

    // DELETE api/discount/{id}/delete
    [HttpDelete("{id:guid}/delete")]
    public Task<IdentityResult> Delete(Guid id)
    {
      return _discountService.DeleteAsync(id);
    }

    // POST api/discount/{id}/infoobject/add
    [HttpPost("{id:guid}/infoobject/add")]
    public Task<IdentityResult> AddInfoObject(Guid id, [FromBody]InfoObjectAddModel apiEntity)
    {
      var entity = InfoObjectAddModel.Map(apiEntity);
      entity.DiscountId = id;

      return _discountService.AddInfoObjectAsync(entity);
    }

    // DELETE api/discount/{discountId}/infoobject/{infoObjectId}/delete
    [HttpDelete("{discountId:guid}/infoobject/{infoObjectId:guid}/delete")]
    public Task<IdentityResult> DeleteInfoObjectFromDiscount(Guid discountId, Guid infoObjectId)
    {
      return _discountService.DeleteInfoObjectFromDiscountAsync(discountId, infoObjectId);
    }

    // PUT api/discount/update/{id}
    [HttpPut("update/{id:guid}")]
    public IdentityResult Put(Guid id, [FromBody]Discount discount)
    {
      return IdentityResult.Success;
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);

      _discountService.Dispose();
    }
  }
}