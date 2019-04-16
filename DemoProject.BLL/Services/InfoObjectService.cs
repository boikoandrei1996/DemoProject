﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL;
using DemoProject.DAL.Enums;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.BLL.Services
{
  public class InfoObjectService : IInfoObjectService
  {
    private readonly IDbContext _context;

    public InfoObjectService(IDbContext context)
    {
      _context = context;
    }

    public Task<bool> ExistAsync(Expression<Func<InfoObject, bool>> filter)
    {
      Check.NotNull(filter, nameof(filter));

      return _context.InfoObjects.AnyAsync(filter);
    }

    public async Task<ServiceResult> AddAsync(InfoObject model)
    {
      Check.NotNull(model, nameof(model));

      var contentGroupExist = await _context.ContentGroups.AnyAsync(x => x.Id == model.ContentGroupId);
      if (contentGroupExist == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(model.ContentGroupId), $"Discount not found with id: '{model.ContentGroupId}'.");
      }

      _context.InfoObjects.Add(model);
      _context.History.Add(ChangeHistory.Create(TableName.InfoObject, ActionType.Add));

      return await _context.SaveAsync(nameof(AddAsync), model);
    }

    public async Task<ServiceResult> UpdateAsync(InfoObject model)
    {
      Check.NotNull(model, nameof(model));

      if (await _context.InfoObjects.AnyAsync(x => x.Id == model.Id) == false)
      {
        return ServiceResultFactory.NotFound;
      }
      else if (await _context.ContentGroups.AnyAsync(x => x.Id == model.ContentGroupId) == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(model.ContentGroupId), $"Discount not found with id: '{model.ContentGroupId}'.");
      }

      _context.InfoObjects.Update(model);
      _context.History.Add(ChangeHistory.Create(TableName.InfoObject, ActionType.Modify));

      return await _context.SaveAsync<InfoObject>(nameof(UpdateAsync));
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var model = await _context.InfoObjects.FirstOrDefaultAsync(x => x.Id == id);
      if (model == null)
      {
        return ServiceResultFactory.Success;
      }

      _context.InfoObjects.Remove(model);
      _context.History.Add(ChangeHistory.Create(TableName.InfoObject, ActionType.Delete));

      return await _context.SaveAsync<InfoObject>(nameof(DeleteAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
