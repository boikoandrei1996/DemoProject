using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL.Enums;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.Shared.Attributes;
using DemoProject.WebApi.Models.DeliveryApiModels;
using DemoProject.WebApi.Models.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  [Route("api/[controller]")]
  [HandleServiceResult]
  [ValidateModelState]
  public sealed class DeliveryController : Controller
  {
    private readonly IContentGroupService _contentGroupService;

    public DeliveryController(
      IContentGroupService contentGroupService)
    {
      _contentGroupService = contentGroupService;
    }

    // GET api/delivery/history
    [HttpGet("history")]
    public async Task<ChangeHistoryViewModel> GetHistoryRecord()
    {
      var entity = await _contentGroupService.GetHistoryRecordAsync(TableName.Delivery);

      return ChangeHistoryViewModel.Map(entity);
    }

    // GET api/delivery/all
    [HttpGet("all")]
    public async Task<IEnumerable<DeliveryViewModel>> GetAll()
    {
      var entities = await _contentGroupService.GetListAsync(ContentGroupName.Delivery);

      return entities.Select(DeliveryViewModel.Map);
    }

    // GET api/delivery/page/{index}
    [HttpGet("page/{index:int?}")]
    public async Task<PageModel<DeliveryViewModel>> GetPage(int? index, [FromQuery]int? pageSize)
    {
      var page = await _contentGroupService.GetPageAsync(
        ContentGroupName.Delivery,
        index >= 1 ? index.Value : Constants.DEFAULT_PAGE_INDEX,
        pageSize >= 1 ? pageSize.Value : Constants.DEFAULT_PAGE_SIZE);

      return PageModel<DeliveryViewModel>.Map(page, DeliveryViewModel.Map);
    }

    // GET api/delivery/{id}
    [HttpGet("{id:guid}")]
    public async Task<DeliveryViewModel> GetOne(Guid id)
    {
      var entity = await _contentGroupService.FindByAsync(ContentGroupName.Delivery, x => x.Id == id);

      return DeliveryViewModel.Map(entity);
    }

    // POST api/delivery
    [HttpPost]
    public async Task<ServiceResult> Add([FromBody]DeliveryAddModel apiEntity)
    {
      var entity = DeliveryAddModel.Map(apiEntity);

      var result = await _contentGroupService.AddAsync(entity);
      if (result.TryCastModel(out ContentGroup contentGroup))
      {
        result.ViewModel = DeliveryViewModel.Map(contentGroup);
      }

      return result;
    }

    // PUT api/delivery/{id}
    [HttpPut("{id:guid}")]
    public async Task<ServiceResult> Edit(Guid id, [FromBody]DeliveryEditModel apiEntity)
    {
      var entity = DeliveryEditModel.Map(apiEntity, id);

      var result = await _contentGroupService.UpdateAsync(entity);
      if (result.TryCastModel(out ContentGroup contentGroup))
      {
        result.ViewModel = DeliveryViewModel.Map(contentGroup);
      }

      return result;
    }

    // DELETE api/delivery/{id}
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