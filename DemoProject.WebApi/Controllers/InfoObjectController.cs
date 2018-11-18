using System;
using System.Threading.Tasks;
using DemoProject.DLL.Interfaces;
using DemoProject.Shared;
using DemoProject.WebApi.Attributes;
using DemoProject.WebApi.Models.InfoObjectApiModels;
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
  public class InfoObjectController : Controller
  {
    private readonly IInfoObjectService _infoObjectService;

    public InfoObjectController(
      IInfoObjectService infoObjectService)
    {
      _infoObjectService = infoObjectService;
    }

    // POST api/infoobject
    [HttpPost]
    public Task<ServiceResult> Add([FromBody]InfoObjectAddModel apiEntity)
    {
      var entity = InfoObjectAddModel.Map(apiEntity);

      return _infoObjectService.AddAsync(entity);
    }

    // PUT api/infoobject/{id}
    [HttpPut("{id:guid}")]
    public Task<ServiceResult> Edit(Guid id, [FromBody]InfoObjectEditModel apiEntity)
    {
      var entity = InfoObjectEditModel.Map(apiEntity, id);

      return _infoObjectService.UpdateAsync(entity);
    }

    // DELETE api/infoobject/{id}
    [HttpDelete("{id:guid}")]
    public Task<ServiceResult> Delete(Guid id)
    {
      return _infoObjectService.DeleteAsync(id);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        _infoObjectService.Dispose();
      }

      base.Dispose(disposing);
    }
  }
}