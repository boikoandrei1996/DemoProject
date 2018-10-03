using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.DLL.Models;

namespace DemoProject.DLL.Interfaces
{
  public interface IMenuItemService : IService<MenuItem>
  {
    Task<List<MenuItem>> GetListAsync(Expression<Func<MenuItem, bool>> filter = null);
    Task<MenuItem> FindByAsync(Expression<Func<MenuItem, bool>> filter);
  }
}
