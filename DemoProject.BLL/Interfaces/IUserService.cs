using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.Shared.Interfaces;

namespace DemoProject.BLL.Interfaces
{
  public interface IUserService : IReadableService<AppUser>
  {
    Task<AppUser> AuthenticateAsync(string username, string password);
    Task<bool> ExistAsync(Expression<Func<AppUser, bool>> filter);
    Task<ServiceResult> AddAsync(AppUser model, string password);
    Task<ServiceResult> UpdatePasswordAsync(Guid id, string newPassword);
    Task<ServiceResult> UpdateAsync(AppUser model);
    Task<ServiceResult> DeleteAsync(Guid id);
  }
}
