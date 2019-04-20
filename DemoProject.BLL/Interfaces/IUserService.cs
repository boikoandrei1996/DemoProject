using System;
using System.Threading.Tasks;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.Shared.Interfaces;

namespace DemoProject.BLL.Interfaces
{
  public interface IUserService : IChangeableService<AppUser, Guid>, IReadableService<AppUser>, IDisposable
  {
    Task<ServiceResult> AuthenticateAsync(string username, string password);
    Task<ServiceResult> AddAsync(AppUser model, string password);
    Task<ServiceResult> UpdatePasswordAsync(Guid id, string newPassword);
  }
}
