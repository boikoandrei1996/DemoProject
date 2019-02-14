using System;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL.Enums;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.Shared.Attributes;
using DemoProject.WebApi.Models.InfoObjectApiModels;
using DemoProject.WebApi.Services;
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
    private readonly ValidationService _validationService;

    public InfoObjectController(
      IInfoObjectService infoObjectService,
      ValidationService validationService)
    {
      _infoObjectService = infoObjectService;
      _validationService = validationService;
    }

    // POST api/infoobject
    [HttpPost]
    public async Task<ServiceResult> Add([FromBody]InfoObjectAddModel apiEntity)
    {
      var type = Enum.Parse<InfoObjectType>(apiEntity.Type, ignoreCase: true);
      if (type == InfoObjectType.Image)
      {
        // validate file exist
        var result = _validationService.ValidateFileExist(apiEntity.Content, nameof(apiEntity.Content));
        if (result != null)
        {
          return result;
        }
      }

      var entity = InfoObjectAddModel.Map(apiEntity);

      return await _infoObjectService.AddAsync(entity);
    }

    // PUT api/infoobject/{id}
    [HttpPut("{id:guid}")]
    public async Task<ServiceResult> Edit(Guid id, [FromBody]InfoObjectEditModel apiEntity)
    {
      if (string.Equals(apiEntity.Type, InfoObjectType.Image.ToString(), StringComparison.OrdinalIgnoreCase))
      {
        // validate file exist
        var result = _validationService.ValidateFileExist(apiEntity.Content, nameof(apiEntity.Content));
        if (result != null)
        {
          return result;
        }
      }

      var entity = InfoObjectEditModel.Map(apiEntity, id);

      return await _infoObjectService.UpdateAsync(entity);
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