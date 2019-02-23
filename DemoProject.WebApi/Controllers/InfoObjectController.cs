using System;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL.Enums;
using DemoProject.Shared;
using DemoProject.Shared.Attributes;
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
    public async Task<ServiceResult> Add([FromBody]InfoObjectAddModel apiEntity)
    {
      var type = Enum.Parse<InfoObjectType>(apiEntity.Type, ignoreCase: true);
      if (type == InfoObjectType.Image)
      {
        // validate file exist
        var fileExistValidationAttribute = new FileExistValidationAttribute(Constants.WEB_CONTENT_ROOT_PATH, Constants.DEFAULT_PATH_TO_IMAGE);
        if (fileExistValidationAttribute.IsNotValid(apiEntity.Content))
        {
          return ServiceResultFactory.BadRequestResult(nameof(apiEntity.Content), FileExistValidationAttribute.CustomMessage);
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
        var fileExistValidationAttribute = new FileExistValidationAttribute(Constants.WEB_CONTENT_ROOT_PATH, Constants.DEFAULT_PATH_TO_IMAGE);
        if (fileExistValidationAttribute.IsNotValid(apiEntity.Content))
        {
          return ServiceResultFactory.BadRequestResult(nameof(apiEntity.Content), FileExistValidationAttribute.CustomMessage);
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