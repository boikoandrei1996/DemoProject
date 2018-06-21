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
    public async Task<IEnumerable<DiscountViewModel>> GetAll([FromQuery]int? page)
    {
      var entities = await _discountService.GetDiscountsAsync();

      return entities.Select(x => DiscountViewModel.Map(x));
    }

    // GET api/discount/page/{index}
    [HttpGet("page/index")]
    public async Task<DiscountPageModel> GetPages(int? index)
    {
      var page = await _discountService.GetPageDiscountsAsync(index ?? 1, 2);

      return DiscountPageModel.Map(page);
    }

    // GET api/discount/{id}
    [HttpGet("{id}")]
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

    // DELETE api/discount/delete/{id}
    [HttpDelete("delete/{id}")]
    public Task<IdentityResult> Delete(Guid id)
    {
      return _discountService.DeleteAsync(id);
    }

    // POST api/discount/{id}/infoobject/add
    [HttpPost("{id}/infoobject/add")]
    public Task<IdentityResult> AddInfoObject(Guid id, [FromBody]InfoObjectAddModel apiEntity)
    {
      var entity = InfoObjectAddModel.Map(apiEntity);

      return _discountService.AddInfoObjectAsync(id, entity);
    }

    // DELETE api/discount/{id}/infoobject/{objectId}/delete
    [HttpDelete("{discountId}/infoobject/{infoObjectId}/delete")]
    public Task<IdentityResult> DeleteInfoObjectFromDiscount(Guid discountId, Guid infoObjectId)
    {
      return _discountService.DeleteInfoObjectFromDiscountAsync(discountId, infoObjectId);
    }

    // PUT api/discount/update/{id}
    [HttpPut("update/{id}")]
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