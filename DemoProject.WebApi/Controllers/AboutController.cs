﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL.Enums;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.Shared.Attributes;
using DemoProject.WebApi.Models.AboutUsApiModels;
using DemoProject.WebApi.Models.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  [Route("api/[controller]")]
  [HandleServiceResult]
  [ValidateModelState]
  public sealed class AboutController : Controller
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
      var entity = await _contentGroupService.GetHistoryRecordAsync(TableName.AboutUs);

      return ChangeHistoryViewModel.Map(entity);
    }

    // GET api/about/all
    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<AboutUsViewModel>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<AboutUsViewModel>> GetAll()
    {
      var entities = await _contentGroupService.GetListAsync(ContentGroupName.AboutUs);

      return entities.Select(AboutUsViewModel.Map);
    }

    // GET api/about/page/{index}
    [HttpGet("page/{index:int?}")]
    [ProducesResponseType(typeof(PageModel<AboutUsViewModel>), StatusCodes.Status200OK)]
    public async Task<PageModel<AboutUsViewModel>> GetPage(int? index, [FromQuery]int? pageSize)
    {
      var page = await _contentGroupService.GetPageAsync(
        ContentGroupName.AboutUs,
        index >= 1 ? index.Value : Constants.DEFAULT_PAGE_INDEX,
        pageSize >= 1 ? pageSize.Value : Constants.DEFAULT_PAGE_SIZE);

      return PageModel<AboutUsViewModel>.Map(page, AboutUsViewModel.Map);
    }

    // GET api/about/{id}
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AboutUsViewModel), StatusCodes.Status200OK)]
    public async Task<AboutUsViewModel> GetOne(Guid id)
    {
      var entity = await _contentGroupService.FindByAsync(ContentGroupName.AboutUs, x => x.Id == id);

      return AboutUsViewModel.Map(entity);
    }

    // POST api/about
    [HttpPost]
    [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status200OK)]
    public async Task<ServiceResult> Add([FromBody]AboutUsAddModel apiEntity)
    {
      var entity = AboutUsAddModel.Map(apiEntity);

      var result = await _contentGroupService.AddAsync(entity);
      if (result.TryCastModel(out ContentGroup contentGroup))
      {
        result.ViewModel = AboutUsViewModel.Map(contentGroup);
      }

      return result;
    }

    // PUT api/about/{id}
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status200OK)]
    public async Task<ServiceResult> Edit(Guid id, [FromBody]AboutUsEditModel apiEntity)
    {
      var entity = AboutUsEditModel.Map(apiEntity, id);

      var result = await _contentGroupService.UpdateAsync(entity);
      if (result.TryCastModel(out ContentGroup contentGroup))
      {
        result.ViewModel = AboutUsViewModel.Map(contentGroup);
      }

      return result;
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