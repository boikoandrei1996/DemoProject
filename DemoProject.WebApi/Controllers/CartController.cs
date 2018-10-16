using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Interfaces;
using DemoProject.DLL.Models;
using DemoProject.WebApi.Attributes;
using DemoProject.WebApi.Models.CartApiModels;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  [Route("api/[controller]")]
  [HandleServiceResult]
  [ValidateModelState]
  public class CartController : Controller
  {
    private readonly ICartService _cartService;

    public CartController(
      ICartService cartService)
    {
      _cartService = cartService;
    }

    // GET api/cart/{id}
    [HttpGet("{id:guid}")]
    public async Task<CartViewModel> GetOne(Guid id)
    {
      var entity = await _cartService.FindByAsync(x => x.Id == id);

      return CartViewModel.Map(entity);
    }

    // POST api/cart
    [HttpPost]
    public Task<ServiceResult> Add([FromBody]CartAddModel apiEntity)
    {
      var entity = CartAddModel.Map(apiEntity);

      return _cartService.AddAsync(entity);
    }

    // PUT api/cart/{id}
    [HttpPut("{id:guid}")]
    public Task<ServiceResult> Edit(Guid id/*, [FromBody]CartEditModel apiEntity*/)
    {
      // var entity = CartEditModel.Map(apiEntity, id);
      var entity = new Cart();

      return _cartService.UpdateAsync(entity);
    }

    // DELETE api/cart/{id}
    [HttpDelete("{id:guid}")]
    public Task<ServiceResult> Delete(Guid id)
    {
      return _cartService.DeleteAsync(id);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        _cartService.Dispose();
      }

      base.Dispose(disposing);
    }
  }
}
