using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DLL.Models;
using DemoProject.Shared;
using DemoProject.WebApi.Attributes;
using DemoProject.WebApi.Models.AboutUsApiModels;
using DemoProject.WebApi.Models.Pages;
using DemoProject.WebApi.Models.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  [Route("api/[controller]")]
  [HandleServiceResult]
  [ValidateModelState]
  public class AboutController : Controller
  {
    private readonly IContentGroupService _contentGroupService;

    public AboutController(
      IContentGroupService contentGroupService)
    {
      _contentGroupService = contentGroupService;
    }

    // GET api/about/history
    [HttpGet("history")]
    [ProducesResponseType(typeof(ChangeHistoryViewModel), StatusCodes.Status200OK)]
    public async Task<ChangeHistoryViewModel> GetHistoryRecord()
    {
      var entity = await _contentGroupService.GetHistoryRecordAsync(GroupName.AboutUs);

      return ChangeHistoryViewModel.Map(entity);
    }

    // GET api/about/all
    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<AboutUsViewModel>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<AboutUsViewModel>> GetAll()
    {
      var entities = await _contentGroupService.GetListAsync(GroupName.AboutUs);

      return entities.Select(AboutUsViewModel.Map);
    }

    // GET api/about/page/{index}
    [HttpGet("page/{index:int?}")]
    [ProducesResponseType(typeof(DiscountPageModel), StatusCodes.Status200OK)]
    public async Task<DiscountPageModel> GetPage(int? index, [FromQuery]int? pageSize)
    {
      var page = await _contentGroupService.GetPageAsync(
        GroupName.AboutUs,
        index >= 1 ? index.Value : Constants.DEFAULT_PAGE_INDEX,
        pageSize >= 1 ? pageSize.Value : Constants.DEFAULT_PAGE_SIZE);

      return DiscountPageModel.Map(page);
    }

    // GET api/about/{id}
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AboutUsViewModel), StatusCodes.Status200OK)]
    public async Task<AboutUsViewModel> GetOne(Guid id)
    {
      var entity = await _contentGroupService.FindByAsync(GroupName.AboutUs, x => x.Id == id);

      return AboutUsViewModel.Map(entity);
    }

    // POST api/about
    [HttpPost]
    [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status200OK)]
    public Task<ServiceResult> Add([FromBody]AboutUsAddModel apiEntity)
    {
      var entity = AboutUsAddModel.Map(apiEntity);

      return _contentGroupService.AddAsync(entity);
    }

    // PUT api/about/{id}
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status200OK)]
    public Task<ServiceResult> Edit(Guid id, [FromBody]AboutUsEditModel apiEntity)
    {
      var entity = AboutUsEditModel.Map(apiEntity, id);

      return _contentGroupService.UpdateAsync(entity);
    }

    // DELETE api/about/{id}
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status200OK)]
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