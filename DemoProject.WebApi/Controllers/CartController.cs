using System;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Interfaces;
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
    public Task<ServiceResult> CreateNew([FromBody]CartAddModel apiEntity)
    {
      var entity = CartAddModel.Map(apiEntity);

      return _cartService.AddAsync(entity);
    }

    // DELETE api/cart/{id}
    [HttpDelete("{id:guid}")]
    public Task<ServiceResult> Delete(Guid id)
    {
      return _cartService.DeleteAsync(id);
    }

    // POST api/cart/addItem
    [HttpPost("addItem")]
    public Task<ServiceResult> AddItemToCart([FromBody]AddItemToCartModel apiEntity)
    {
      return _cartService.AddItemToCartAsync(apiEntity.CartId, apiEntity.ShopItemDetailId);
    }

    // POST api/cart/removeItem
    [HttpPost("removeItem")]
    public Task<ServiceResult> RemoveItemFromCart([FromBody]RemoveItemFromCartModel apiEntity)
    {
      return _cartService.RemoveItemFromCartAsync(apiEntity.CartId, apiEntity.ShopItemDetailId, apiEntity.ShouldBeRemovedAllItems);
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
