using System;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Interfaces;
using DemoProject.WebApi.Infrastructure;
using DemoProject.WebApi.Models.InfoObjectApiModels;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  [Route("api/[controller]")]
  [HandleServiceResult]
  /*[ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ServiceError>))]*/
  public class InfoObjectController : Controller
  {
    private readonly IDiscountService _discountService;
    private readonly IInfoObjectService _infoObjectService;

    public InfoObjectController(
      IDiscountService discountService,
      IInfoObjectService infoObjectService)
    {
      _discountService = discountService;
      _infoObjectService = infoObjectService;
    }

    // POST api/infoobject
    [HttpPost]
    public async Task<ServiceResult> Add([FromBody]InfoObjectAddModel apiEntity)
    {
      if (!ModelState.IsValid)
      {
        return ServiceResultFactory.BadRequestResult(ModelState);
      }

      var entity = InfoObjectAddModel.Map(apiEntity);

      return await _infoObjectService.AddAsync(entity);
    }

    // DELETE api/infoobject/{id}
    [HttpDelete("{id:guid}")]
    public Task<ServiceResult> Delete(Guid id)
    {
      return _infoObjectService.DeleteAsync(id);
    }

    // PUT api/infoobject/{id}
    [HttpPut("{id:guid}")]
    public async Task<ServiceResult> Edit(Guid id, [FromBody]InfoObjectEditModel apiEntity)
    {
      if (!ModelState.IsValid)
      {
        return ServiceResultFactory.BadRequestResult(ModelState);
      }

      var entity = InfoObjectEditModel.Map(apiEntity, id);

      return await _infoObjectService.UpdateAsync(entity);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        _discountService.Dispose();
        _infoObjectService.Dispose();
      }

      base.Dispose(disposing);
    }
  }
}