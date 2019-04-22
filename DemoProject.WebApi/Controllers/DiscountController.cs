using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL.Enums;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.Shared.Attributes;
using DemoProject.WebApi.Models.DiscountApiModels;
using DemoProject.WebApi.Models.Shared;
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
  public sealed class DiscountController : Controller
  {
    private readonly IContentGroupService _contentGroupService;

    public DiscountController(
      IContentGroupService contentGroupService)
    {
      _contentGroupService = contentGroupService;
    }

    // GET api/discount/history
    [HttpGet("history")]
    public async Task<ChangeHistoryViewModel> GetHistoryRecord()
    {
      var entity = await _contentGroupService.GetHistoryRecordAsync(TableName.Discount);

      return ChangeHistoryViewModel.Map(entity);
    }

    // GET api/discount/all
    [HttpGet("all")]
    public async Task<IEnumerable<DiscountViewModel>> GetAll()
    {
      var entities = await _contentGroupService.GetListAsync(ContentGroupName.Discount);

      return entities.Select(DiscountViewModel.Map);
    }

    // GET api/discount/page/{index}
    [HttpGet("page/{index:int?}")]
    public async Task<PageModel<DiscountViewModel>> GetPage(int? index, [FromQuery]int? pageSize)
    {
      var page = await _contentGroupService.GetPageAsync(
        ContentGroupName.Discount,
        index >= 1 ? index.Value : Constants.DEFAULT_PAGE_INDEX,
        pageSize >= 1 ? pageSize.Value : Constants.DEFAULT_PAGE_SIZE);

      return PageModel<DiscountViewModel>.Map(page, DiscountViewModel.Map);
    }

    // GET api/discount/{id}
    [HttpGet("{id:guid}")]
    public async Task<DiscountViewModel> GetOne(Guid id)
    {
      var entity = await _contentGroupService.FindByAsync(ContentGroupName.Discount, x => x.Id == id);

      return DiscountViewModel.Map(entity);
    }

    // POST api/discount
    [HttpPost]
    public async Task<ServiceResult> Add([FromBody]DiscountAddModel apiEntity)
    {
      var entity = DiscountAddModel.Map(apiEntity);

      var result = await _contentGroupService.AddAsync(entity);
      if (result.TryCastModel(out ContentGroup contentGroup))
      {
        result.ViewModel = DiscountViewModel.Map(contentGroup);
      }

      return result;
    }

    // PUT api/discount/{id}
    [HttpPut("{id:guid}")]
    public async Task<ServiceResult> Edit(Guid id, [FromBody]DiscountEditModel apiEntity)
    {
      var entity = DiscountEditModel.Map(apiEntity, id);

      var result = await _contentGroupService.UpdateAsync(entity);
      if (result.TryCastModel(out ContentGroup contentGroup))
      {
        result.ViewModel = DiscountViewModel.Map(contentGroup);
      }

      return result;
    }

    // DELETE api/discount/{id}
    [HttpDelete("{id:guid}")]
    public Task<ServiceResult> Delete(Guid id)
    {
      return _contentGroupService.DeleteAsync(id);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        _contentGroupService.Dispose();
      }

      base.Dispose(disposing);
    }
  }
}