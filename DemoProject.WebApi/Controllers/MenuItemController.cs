using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.Shared;
using DemoProject.Shared.Attributes;
using DemoProject.WebApi.Models.MenuItemApiModels;
using DemoProject.WebApi.Models.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  [Route("api/[controller]")]
  [HandleServiceResult]
  [ValidateModelState]
  public class MenuItemController : Controller
  {
    private readonly IMenuItemService _menuItemService;

    public MenuItemController(
      IMenuItemService menuItemService)
    {
      _menuItemService = menuItemService;
    }

    // GET api/menuitem/history
    [HttpGet("history")]
    public async Task<ChangeHistoryViewModel> GetHistoryRecord()
    {
      var entity = await _menuItemService.GetHistoryRecordAsync();

      return ChangeHistoryViewModel.Map(entity);
    }

    // GET api/menuitem/all
    [HttpGet("all")]
    public async Task<IEnumerable<MenuItemViewModel>> GetAll()
    {
      var entities = await _menuItemService.GetListAsync();

      return entities.Select(MenuItemViewModel.Map);
    }

    // GET api/menuitem/{id}
    [HttpGet("{id:guid}")]
    public async Task<MenuItemViewModel> GetOne(Guid id)
    {
      var entity = await _menuItemService.FindByAsync(x => x.Id == id);

      return MenuItemViewModel.Map(entity);
    }

    // POST api/menuitem
    [HttpPost]
    public Task<ServiceResult> Add([FromBody]MenuItemAddModel apiEntity)
    {
      var entity = MenuItemAddModel.Map(apiEntity);

      return _menuItemService.AddAsync(entity);
    }

    // PUT api/menuitem/{id}
    [HttpPut("{id:guid}")]
    public Task<ServiceResult> Edit(Guid id, [FromBody]MenuItemEditModel apiEntity)
    {
      var entity = MenuItemEditModel.Map(apiEntity, id);

      return _menuItemService.UpdateAsync(entity);
    }

    // DELETE api/menuitem/{id}
    [HttpDelete("{id:guid}")]
    public Task<ServiceResult> Delete(Guid id)
    {
      return _menuItemService.DeleteAsync(id);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        _menuItemService.Dispose();
      }

      base.Dispose(disposing);
    }
  }
}
