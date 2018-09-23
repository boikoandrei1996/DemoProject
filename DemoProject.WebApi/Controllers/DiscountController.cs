using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Interfaces;
using DemoProject.WebApi.Attributes;
using DemoProject.WebApi.Models.DiscountApiModels;
using DemoProject.WebApi.Models.Pages;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  [Route("api/[controller]")]
  [HandleServiceResult]
  [ValidateModelState]
  /*[ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ServiceError>))]*/
  public class DiscountController : Controller
  {
    private const int DEFAULT_PAGE_INDEX = 1;
    private const int DEFAULT_PAGE_SIZE = 2;

    private readonly IDiscountService _discountService;

    public DiscountController(
      IDiscountService discountService)
    {
      _discountService = discountService;
    }

    // GET api/discount/history
    [HttpGet("history")]
    public async Task<ChangeHistoryViewModel> GetHistoryRecord()
    {
      var entity = await _discountService.GetHistoryRecordAsync();

      return ChangeHistoryViewModel.Map(entity);
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

    // POST api/discount
    [HttpPost]
    public Task<ServiceResult> Add([FromBody]DiscountAddModel apiEntity)
    {
      var entity = DiscountAddModel.Map(apiEntity);

      return _discountService.AddAsync(entity);
    }

    // PUT api/discount/{id}
    [HttpPut("{id:guid}")]
    public Task<ServiceResult> Edit(Guid id, [FromBody]DiscountEditModel apiEntity)
    {
      var entity = DiscountEditModel.Map(apiEntity, id);

      return _discountService.UpdateAsync(entity);
    }

    // DELETE api/discount/{id}
    [HttpDelete("{id:guid}")]
    public Task<ServiceResult> Delete(Guid id)
    {
      return _discountService.DeleteAsync(id);
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