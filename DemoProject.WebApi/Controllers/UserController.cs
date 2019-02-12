using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.Shared.Attributes;
using DemoProject.WebApi.Infrastructure;
using DemoProject.WebApi.Models.Pages;
using DemoProject.WebApi.Models.UserApiModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  /*
  // multiple roles restrictment
  [Authorize(Roles = Role.Admin + "," + Role.Moderator)]

  // only allow admins to access other user records
  var currentUserId = int.Parse(User.Identity.Name);
  if (id != currentUserId && !User.IsInRole(Role.Admin))
  {
    return Forbid();
  }
  */

  [Route("api/[controller]")]
  [HandleServiceResult]
  [ValidateModelState]
  public class UserController : Controller
  {
    private readonly IUserService _userService;
    private readonly AuthTokenGenerator _tokenGenerator;

    public UserController(
      IUserService userService,
      AuthTokenGenerator tokenGenerator)
    {
      _userService = userService;
      _tokenGenerator = tokenGenerator;
    }

    // GET api/user/all
    [HttpGet("all")]
    public async Task<IEnumerable<UserViewModel>> GetAll()
    {
      var entities = await _userService.GetListAsync();

      return entities.Select(UserViewModel.Map);
    }

    // GET api/user/page/{index}
    [HttpGet("page/{index:int?}")]
    public async Task<UserPageModel> GetPage(int? index, [FromQuery]int? pageSize)
    {
      var page = await _userService.GetPageAsync(
        index >= 1 ? index.Value : Constants.DEFAULT_PAGE_INDEX,
        pageSize >= 1 ? pageSize.Value : Constants.DEFAULT_PAGE_SIZE);

      return UserPageModel.Map(page);
    }

    // GET api/user/{id}
    [HttpGet("{id:guid}")]
    public async Task<UserViewModel> GetOne(Guid id)
    {
      var entity = await _userService.FindByAsync(x => x.Id == id);

      return UserViewModel.Map(entity);
    }

    // POST api/user/authenticate
    [HttpPost("authenticate")]
    public async Task<UserAuthResponseModel> Authenticate([FromBody]UserAuthRequestModel apiEntity)
    {
      var user = await _userService.AuthenticateAsync(apiEntity.Username, apiEntity.Password);
      if (user == null)
      {
        return null;
      }

      var token = _tokenGenerator.GenerateNewToken(user.Id, user.Role);

      return UserAuthResponseModel.Map(user, token);
    }

    // POST api/user
    [HttpPost]
    public Task<ServiceResult> CreateNew([FromBody]UserAddModel apiEntity)
    {
      var entity = UserAddModel.Map(apiEntity);

      return _userService.AddAsync(entity, apiEntity.Password);
    }

    // PUT api/user/{id}
    [HttpPut("{id:guid}")]
    public Task<ServiceResult> UpdatePassword(Guid id, [FromBody]string newPassword)
    {
      return _userService.UpdatePasswordAsync(id, newPassword);
    }

    // DELETE api/user/{id}
    [HttpDelete("{id:guid}")]
    public Task<ServiceResult> Delete(Guid id)
    {
      return _userService.DeleteAsync(id);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        _userService.Dispose();
      }

      base.Dispose(disposing);
    }
  }
}
