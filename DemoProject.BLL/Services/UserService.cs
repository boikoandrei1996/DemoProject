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
  public sealed class UserService : BaseService<AppUser>, IUserService
  {
    private readonly IPasswordManager _passwordManager;

    public UserService(
      IDbContext context,
      IPasswordManager passwordManager) : base(context)
    {
      _passwordManager = passwordManager;
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

    public async Task<ServiceResult> AuthenticateAsync(string username, string password)
    {
      Check.NotNullOrEmpty(username, nameof(username));
      Check.NotNullOrEmpty(password, nameof(password));

      var user = await this.FindByAsync(x => x.Username == username);
      if (user == null)
      {
        return ServiceResultFactory.BadRequestResult(nameof(AuthenticateAsync), "Username was not found.");
      }

      var success = _passwordManager.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
      if (success == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(AuthenticateAsync), "Password is wrong.");
      }

      return ServiceResultFactory.SuccessResult(user);
    }

    public async Task<ServiceResult> AddAsync(AppUser model, string password)
    {
      Check.NotNull(model, nameof(model));
      Check.NotNullOrEmpty(model.Username, nameof(model.Username));
      Check.NotNullOrEmpty(model.Email, nameof(model.Email));
      Check.NotNullOrEmpty(password, nameof(password));

      var usernameExist = await this.ExistAsync(x => model.Username.Equals(x.Username, StringComparison.OrdinalIgnoreCase));
      if (usernameExist)
      {
        return ServiceResultFactory.BadRequestResult(nameof(AddAsync), "Username is already taken.");
      }

      var emailExist = await this.ExistAsync(x => model.Email.Equals(x.Email, StringComparison.OrdinalIgnoreCase));
      if (emailExist)
      {
        return ServiceResultFactory.BadRequestResult(nameof(AddAsync), "Email is already taken.");
      }

      model.Role = Role.Normalize(model.Role);
      if (string.IsNullOrEmpty(model.Role))
      {
        return ServiceResultFactory.BadRequestResult(nameof(AddAsync), "Incorrect role name.");
      }

      var (passwordHash, passwordSalt) = _passwordManager.CreatePasswordHash(password);
      model.PasswordHash = passwordHash;
      model.PasswordSalt = passwordSalt;

      _context.Users.Add(model);

      var result = await _context.SaveAsync(nameof(AddAsync));
      result.SetModelIfSuccess(model);

      return result;
    }

    public async Task<ServiceResult> ConfirmEmailAsync(string email)
    {
      Check.NotNullOrEmpty(email, nameof(email));

      var user = await this.FindByAsync(x => email.Equals(x.Email, StringComparison.OrdinalIgnoreCase));
      if (user == null)
      {
        return ServiceResultFactory.NotFound;
      }

      user.EmailConfirmed = true;
      user.LastModified = DateTime.UtcNow;

      _context.Users.Update(user);

      var result = await _context.SaveAsync(nameof(UpdatePasswordAsync));
      result.SetModelIfSuccess(user);

      return result;
    }

    public async Task<ServiceResult> UpdatePasswordAsync(Guid id, string newPassword)
    {
      Check.NotNullOrEmpty(newPassword, nameof(newPassword));

      var user = await this.FindByAsync(x => x.Id == id);
      if (user == null)
      {
        return ServiceResultFactory.NotFound;
      }

      var (passwordHash, passwordSalt) = _passwordManager.CreatePasswordHash(newPassword);

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;
      user.LastModified = DateTime.UtcNow;

      _context.Users.Update(user);

      var result = await _context.SaveAsync(nameof(UpdatePasswordAsync));
      result.SetModelIfSuccess(user);

      return result;
    }

    public async Task<ServiceResult> UpdateRoleAsync(Guid id, string newRole)
    {
      Check.NotNullOrEmpty(newRole, nameof(newRole));

      var user = await this.FindByAsync(x => x.Id == id);
      if (user == null)
      {
        return ServiceResultFactory.NotFound;
      }

      newRole = Role.Normalize(newRole);
      if (string.IsNullOrEmpty(newRole))
      {
        return ServiceResultFactory.BadRequestResult(nameof(UpdateRoleAsync), "Incorrect role name.");
      }

      user.Role = newRole;
      user.LastModified = DateTime.UtcNow;

      _context.Users.Update(user);

      var result = await _context.SaveAsync(nameof(UpdateRoleAsync));
      result.SetModelIfSuccess(user);

      return result;
    }

    public async Task<ServiceResult> UpdateAsync(AppUser model)
    {
      Check.NotNull(model, nameof(model));

      var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.Id);
      if (user == null)
      {
        return ServiceResultFactory.NotFound;
      }

      var changed = false;

      // update username
      if (Utility.IsModified(user.Username, model.Username))
      {
        var usernameExist = await this.ExistAsync(x => x.Username == model.Username);
        if (usernameExist)
        {
          return ServiceResultFactory.BadRequestResult(nameof(UpdateAsync), "Username is already taken.");
        }

        user.Username = model.Username;
        changed = true;
      }

      // update email
      if (Utility.IsModified(user.Email, model.Email))
      {
        var emailExist = await this.ExistAsync(x => x.Email == model.Email);
        if (emailExist)
        {
          return ServiceResultFactory.BadRequestResult(nameof(UpdateAsync), "Email is already taken.");
        }

        user.Email = model.Email;
        user.EmailConfirmed = false;
        changed = true;
      }

      // update firstname
      if (Utility.IsModified(user.FirstName, model.FirstName))
      {
        user.FirstName = model.FirstName;
        changed = true;
      }

      // update lastname
      if (Utility.IsModified(user.LastName, model.LastName))
      {
        user.LastName = model.LastName;
        changed = true;
      }

      // update phonenumber
      if (Utility.IsModified(user.PhoneNumber, model.PhoneNumber))
      {
        user.PhoneNumber = model.PhoneNumber;
        changed = true;
      }

      if (changed == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(UpdateAsync), "Nothing to update.");
      }

      user.LastModified = DateTime.UtcNow;
      _context.Users.Update(user);

      var result = await _context.SaveAsync(nameof(UpdateAsync));
      result.SetModelIfSuccess(user);

      return result;
    }

    public Task<ServiceResult> AddAsync(AppUser model)
    {
      throw new NotImplementedException();
    }
  }
}
