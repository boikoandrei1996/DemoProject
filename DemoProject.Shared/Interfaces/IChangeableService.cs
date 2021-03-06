﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DemoProject.Shared.Interfaces
{
  public interface IChangeableService<TModel, TModelId>
  {
    Task<bool> ExistAsync(Expression<Func<TModel, bool>> filter);
    Task<ServiceResult> AddAsync(TModel model);
    Task<ServiceResult> UpdateAsync(TModel model);
    Task<ServiceResult> DeleteAsync(TModelId id);
  }
}
