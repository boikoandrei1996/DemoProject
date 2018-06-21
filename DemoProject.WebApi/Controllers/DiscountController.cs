using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.DLL.Interfaces;
using DemoProject.DLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  [Route("api/[controller]")]
  public class DiscountController : Controller
  {
    private readonly IDiscountService _discountService;

    public DiscountController(IDiscountService discountService)
    {
      _discountService = discountService;
    }

    // GET api/discount/all
    [HttpGet("all")]
    public async Task<IEnumerable<Discount>> GetAll()
    {
      var entities = await _discountService.GetDiscountsAsync();

      return entities;
    }

    // GET api/discount/id
    [HttpGet("{id}")]
    public async Task<Discount> Get(Guid id)
    {
      var entity = await _discountService.FindByAsync(x => x.Id == id);

      return entity;
    }

    // POST api/discount/add
    [HttpPost("add")]
    public Task<IdentityResult> Post([FromBody]Discount discount)
    {
      var result = _discountService.AddAsync(discount);

      return result;
    }

    // PUT api/discount/update/id
    [HttpPut("update/{id}")]
    public Task<IdentityResult> Put(Guid id, [FromBody]Discount discount)
    {
      var result = _discountService.UpdateAsync(id, discount);

      return result;
    }

    // DELETE api/discount/delete/id
    [HttpDelete("delete/{id}")]
    public Task<IdentityResult> Delete(Guid id)
    {
      var result = _discountService.DeleteAsync(id);

      return result;
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);

      _discountService.Dispose();
    }
  }
}