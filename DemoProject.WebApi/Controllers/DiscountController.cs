using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Interfaces;
using DemoProject.WebApi.Infrastructure;
using DemoProject.WebApi.Models.DiscountApiModels;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  [Route("api/[controller]")]
  [HandleServiceResult]
  /*[ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ServiceError>))]*/
  public class DiscountController : Controller
  {
    private const int DEFAULT_PAGE_INDEX = 1;
    private const int DEFAULT_PAGE_SIZE = 2;

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

      return entities.Select(DiscountViewModel.Map);
    }

    // GET api/discount/page/{index}
    [HttpGet("page/{index:int?}")]
    public async Task<DiscountPageModel> GetPage(int? index, [FromQuery]int? pageSize)
    {
      var page = await _discountService.GetPageDiscountsAsync(
        index >= 1 ? index.Value : DEFAULT_PAGE_INDEX,
        pageSize >= 1 ? pageSize.Value : DEFAULT_PAGE_SIZE);

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
    public Task<ServiceResult> Add([FromBody]DiscountAddModel apiEntity)
    {
      if (!ModelState.IsValid)
      {
        return Task.Run(() => ServiceResultFactory.BadRequestResult(ModelState));
      }

      var entity = DiscountAddModel.Map(apiEntity);

      return _discountService.AddAsync(entity);
    }

    // DELETE api/discount/{id}/delete
    [HttpDelete("{id:guid}/delete")]
    public Task<ServiceResult> Delete(Guid id)
    {
      return _discountService.DeleteAsync(id);
    }

    // POST api/discount/{id}/infoobject/add
    [HttpPost("{id:guid}/infoobject/add")]
    public async Task<ServiceResult> AddInfoObject(Guid id, [FromBody]InfoObjectAddModel apiEntity)
    {
      if (!ModelState.IsValid)
      {
        return ServiceResultFactory.BadRequestResult(ModelState);
      }

      if (await _discountService.ExistDiscountAsync(x => x.Id == id) == false)
      {
        return ServiceResultFactory.NotFound;
      }

      var entity = InfoObjectAddModel.Map(apiEntity);
      entity.DiscountId = id;

      return await _discountService.AddInfoObjectAsync(entity);
    }

    // DELETE api/discount/infoobject/{id}/delete
    [HttpDelete("infoobject/{id:guid}/delete")]
    public Task<ServiceResult> DeleteInfoObjectFromDiscount(Guid id)
    {
      return _discountService.DeleteInfoObjectAsync(id);
    }

    // PUT api/discount/{id}/update
    [HttpPut("{id:guid}/update")]
    public async Task<ServiceResult> EditDiscount(Guid id, [FromBody]DiscountEditModel apiEntity)
    {
      if (!ModelState.IsValid)
      {
        return ServiceResultFactory.BadRequestResult(ModelState);
      }

      if (await _discountService.ExistDiscountAsync(x => x.Id == id) == false)
      {
        return ServiceResultFactory.NotFound;
      }

      var entity = DiscountEditModel.Map(apiEntity);
      entity.Id = id;

      return await _discountService.UpdateAsync(entity);
    }

    // PUT api/discount/infoobject/{id}/update
    [HttpPut("{id:guid}/update")]
    public async Task<ServiceResult> EditInfoObject(Guid id, [FromBody]InfoObjectEditModel apiEntity)
    {
      if (!ModelState.IsValid)
      {
        return ServiceResultFactory.BadRequestResult(ModelState);
      }

      if (await _discountService.ExistInfoObjectAsync(x => x.Id == id) == false)
      {
        return ServiceResultFactory.NotFound;
      }

      if (await _discountService.ExistDiscountAsync(x => x.Id == apiEntity.DiscountId) == false)
      {
        return ServiceResultFactory.BadRequestResult("Discount not found.");
      }

      var entity = InfoObjectEditModel.Map(apiEntity);
      entity.Id = id;

      return await _discountService.UpdateInfoObjectAsync(entity);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        _discountService.Dispose();
      }

      base.Dispose(disposing);
    }
  }
}