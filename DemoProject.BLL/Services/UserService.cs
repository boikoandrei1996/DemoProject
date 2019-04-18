using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.BLL.Services
{
  public class UserService : IUserService
  {
    private readonly IDbContext _context;
    private readonly IPasswordManager _passwordManager;

    public UserService(
      IDbContext context,
      IPasswordManager passwordManager)
    {
      _context = context;
      _passwordManager = passwordManager;
    }

    public async Task<AppUser> AuthenticateAsync(string username, string password)
    {
      Check.NotNullOrEmpty(username, nameof(username));
      Check.NotNullOrEmpty(password, nameof(password));

      var user = await this.FindByAsync(x => x.Username == username);
      if (user != null)
      {
        var success = _passwordManager.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
        if (success)
        {
          return user;
        }
      }

      return null;
    }

    public async Task<Page<AppUser>> GetPageAsync(int pageIndex, int pageSize, Expression<Func<AppUser, bool>> filter = null)
    {
      Check.Positive(pageIndex, nameof(pageIndex));
      Check.Positive(pageSize, nameof(pageSize));

      var query = _context.Users.AsNoTracking();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      var totalCount = await query.CountAsync();

      var page = Page<AppUser>.Create(pageSize, pageIndex, totalCount);

      if (totalCount != 0)
      {
        page.Records = await query
          .Skip((page.Current - 1) * page.Size)
          .Take(page.Size)
          .Include(x => x.ApprovedOrders)
          .Include(x => x.RejectedOrders)
          .Include(x => x.ClosedOrders)
          .ToListAsync();
      }

      return page;
    }

    public Task<List<AppUser>> GetListAsync(Expression<Func<AppUser, bool>> filter = null)
    {
      var query = _context.Users.AsNoTracking();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      return query
        .Include(x => x.ApprovedOrders)
        .Include(x => x.RejectedOrders)
        .Include(x => x.ClosedOrders)
        .ToListAsync();
    }

    public Task<AppUser> FindByAsync(Expression<Func<AppUser, bool>> filter)
    {
      Check.NotNull(filter, nameof(filter));

      return _context.Users.AsNoTracking()
        .Include(x => x.ApprovedOrders)
        .Include(x => x.RejectedOrders)
        .Include(x => x.ClosedOrders)
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
      Check.NotNullOrEmpty(password, nameof(password));

      var usernameExist = await this.ExistAsync(x => x.Username == model.Username);
      if (usernameExist)
      {
        return ServiceResultFactory.BadRequestResult(nameof(AddAsync), "Username is already taken.");
      }

      var emailExist = await this.ExistAsync(x => x.Email == model.Email);
      if (emailExist)
      {
        return ServiceResultFactory.BadRequestResult(nameof(AddAsync), "Email is already taken.");
      }

      var role = Role.Normalize(model.Role);
      if (string.IsNullOrEmpty(role))
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

      return await _context.SaveAsync(nameof(AddAsync), model);
    }

    public async Task<ServiceResult> UpdatePasswordAsync(Guid id, string newPassword)
    {
      Check.NotNullOrEmpty(newPassword, nameof(newPassword));

      var user = await this.FindByAsync(x => x.Id == id);
      if (user == null)
      {
        return ServiceResultFactory.BadRequestResult(nameof(UpdatePasswordAsync), $"User not found with id: {id}.");
      }

      var (passwordHash, passwordSalt) = _passwordManager.CreatePasswordHash(newPassword);

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;
      user.LastModified = DateTime.UtcNow;

      _context.Users.Update(user);

      return await _context.SaveAsync<AppUser>(nameof(UpdatePasswordAsync), null);
    }

    public Task<ServiceResult> UpdateAsync(AppUser model)
    {
      throw new NotImplementedException();
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var model = await this.FindByAsync(x => x.Id == id);
      if (model == null)
      {
        return ServiceResultFactory.Success;
      }

      _context.Users.Remove(model);

      return await _context.SaveAsync<AppUser>(nameof(DeleteAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
