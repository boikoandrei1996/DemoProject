using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.BLL.PageModels;
using DemoProject.DAL;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.Shared.Extensions;
using DemoProject.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.BLL.Services
{
  public class UserService : IUserService
  {
    private readonly EFContext _context;
    private readonly IPasswordManager _passwordManager;

    public UserService(
      EFContext context,
      IPasswordManager passwordManager)
    {
      _context = context;
      _passwordManager = passwordManager;
    }

    public async Task<AppUser> AuthenticateAsync(string username, string password)
    {
      if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
      {
        return null;
      }

      var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
      if (user == null)
      {
        return null;
      }

      if (_passwordManager.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
      {
        // authentication successful
        return user;
      }

      // authentication failed
      return null;
    }

    public async Task<IPage<AppUser>> GetPageAsync(int pageIndex, int pageSize, Expression<Func<AppUser, bool>> filter = null)
    {
      var query = _context.Users.AsNoTracking();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      var totalCount = await query.CountAsync();

      var page = new UserPage
      {
        CurrentPage = pageIndex,
        PageSize = pageSize,
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
      };

      page.Records = await query
        .Skip((pageIndex - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

      return page;
    }

    public async Task<List<AppUser>> GetListAsync(Expression<Func<AppUser, bool>> filter = null)
    {
      var query = _context.Users.AsNoTracking();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      return await query.ToListAsync();
    }

    public Task<AppUser> FindByAsync(Expression<Func<AppUser, bool>> filter)
    {
      Check.NotNull(filter, nameof(filter));

      return _context.Users.AsNoTracking()
        .FirstOrDefaultAsync(filter);
    }

    public Task<bool> ExistAsync(Expression<Func<AppUser, bool>> filter)
    {
      Check.NotNull(filter, nameof(filter));

      return _context.Users.AnyAsync(filter);
    }

    public async Task<ServiceResult> AddAsync(AppUser model, string password)
    {
      Check.NotNull(model, nameof(model));
      
      if (string.IsNullOrWhiteSpace(password))
      {
        return ServiceResultFactory.BadRequestResult(nameof(AddAsync), "Incorrect password.");
      }

      if (await _context.Users.AnyAsync(x => x.Username == model.Username))
      {
        return ServiceResultFactory.BadRequestResult(nameof(AddAsync), "Username is already taken.");
      }

      if (await _context.Users.AnyAsync(x => x.Email == model.Email))
      {
        return ServiceResultFactory.BadRequestResult(nameof(AddAsync), "Email is already taken.");
      }

      var role = Role.NormalizeRoleName(model.Role);
      if (role == null)
      {
        return ServiceResultFactory.BadRequestResult(nameof(AddAsync), "Incorrect role name.");
      }
      else
      {
        model.Role = role;
      }

      var (passwordHash, passwordSalt) = _passwordManager.CreatePasswordHash(password);
      model.PasswordHash = passwordHash;
      model.PasswordSalt = passwordSalt;

      _context.Users.Add(model);

      return await _context.SaveAsync(nameof(AddAsync), model.Id);
    }

    public async Task<ServiceResult> UpdatePasswordAsync(Guid id, string newPassword)
    {
      if (string.IsNullOrWhiteSpace(newPassword))
      {
        return ServiceResultFactory.BadRequestResult(nameof(UpdatePasswordAsync), "Incorrect password.");
      }

      var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
      if (user == null)
      {
        return ServiceResultFactory.BadRequestResult(nameof(UpdatePasswordAsync), $"User not found with id: {id}.");
      }

      var (passwordHash, passwordSalt) = _passwordManager.CreatePasswordHash(newPassword);
      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      user.LastModified = DateTime.UtcNow;

      _context.Users.Update(user);

      return await _context.SaveAsync(nameof(UpdatePasswordAsync), id);
    }

    public Task<ServiceResult> UpdateAsync(AppUser model)
    {
      throw new NotImplementedException();
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var model = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
      if (model == null)
      {
        return ServiceResultFactory.Success;
      }

      _context.Users.Remove(model);

      return await _context.SaveAsync(nameof(DeleteAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
