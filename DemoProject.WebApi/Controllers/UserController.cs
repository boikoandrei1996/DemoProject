using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.Shared.Attributes;
using DemoProject.WebApi.Infrastructure;
using DemoProject.WebApi.Models.Shared;
using DemoProject.WebApi.Models.UserApiModels;
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
  public sealed class UserController : Controller
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
    public async Task<PageModel<UserViewModel>> GetPage(int? index, [FromQuery]int? pageSize)
    {
      var page = await _userService.GetPageAsync(
        index >= 1 ? index.Value : Constants.DEFAULT_PAGE_INDEX,
        pageSize >= 1 ? pageSize.Value : Constants.DEFAULT_PAGE_SIZE);

      return PageModel<UserViewModel>.Map(page, UserViewModel.Map);
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
    public async Task<ServiceResult> Authenticate([FromBody]UserAuthModel apiEntity)
    {
      var result = await _userService.AuthenticateAsync(apiEntity.Username, apiEntity.Password);
      if (result.TryCastModel(out AppUser user))
      {
        var token = _tokenGenerator.GenerateNewToken(user.Id, user.Role);
        result.ViewModel = UserAuthViewModel.Map(user, token);
      }

      return result;
    }

    // POST api/user
    [HttpPost]
    public async Task<ServiceResult> CreateNew([FromBody]UserAddModel apiEntity)
    {
      var entity = UserAddModel.Map(apiEntity);

      var result = await _userService.AddAsync(entity, apiEntity.Password);
      if (result.TryCastModel(out AppUser user))
      {
        result.ViewModel = UserViewModel.Map(user);
      }

      return result;
    }

    // PUT api/user/{id}
    [HttpPut("{id:guid}")]
    public async Task<ServiceResult> Edit(Guid id, [FromBody]UserEditModel apiEntity)
    {
      var entity = UserEditModel.Map(id, apiEntity);

      var result = await _userService.UpdateAsync(entity);
      if (result.TryCastModel(out AppUser user))
      {
        result.ViewModel = UserViewModel.Map(user);
      }

      return result;
    }

    // PUT api/user/{id}/password
    [HttpPut("{id:guid}/password")]
    public async Task<ServiceResult> UpdatePassword(Guid id, [FromBody]UserPasswordUpdateModel apiEntity)
    {
      var result = await _userService.AuthenticateAsync(id, apiEntity.OldPassword);
      if (result.IsSuccess)
      {
        result = await _userService.UpdatePasswordAsync(id, apiEntity.NewPassword);
        if (result.TryCastModel(out AppUser user))
        {
          result.ViewModel = UserViewModel.Map(user);
        }
      }

      return result;
    }

    // PUT api/user/{id}/role
    [HttpPut("{id:guid}/role")]
    public async Task<ServiceResult> UpdateRole(Guid id, [FromBody]UserRoleUpdateModel apiEntity)
    {
      var result = await _userService.UpdateRoleAsync(id, apiEntity.NewRole);
      if (result.TryCastModel(out AppUser user))
      {
        result.ViewModel = UserViewModel.Map(user);
      }

      return result;
    }

    // PUT api/user/confirm/email
    [HttpPut("confirm/email")]
    public async Task<ServiceResult> ConfirmEmail([FromBody]UserConfirmEmailUpdateModel apiEntity)
    {
      var result = await _userService.ConfirmEmailAsync(apiEntity.Email);
      if (result.TryCastModel(out AppUser user))
      {
        result.ViewModel = UserViewModel.Map(user);
      }

      return result;
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
