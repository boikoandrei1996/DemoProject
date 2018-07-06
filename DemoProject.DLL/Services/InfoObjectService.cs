﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.DLL.Extensions;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Interfaces;
using DemoProject.DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DLL.Services
{
  public class InfoObjectService : IInfoObjectService
  {
    private readonly EFContext _context;

    public InfoObjectService(EFContext context)
    {
      _context = context;
    }

    public Task<bool> ExistAsync(Expression<Func<InfoObject, bool>> filter)
    {
      if (filter == null)
      {
        throw new ArgumentNullException(nameof(filter));
      }

      return _context.InfoObjects.AnyAsync(filter);
    }

    public Task<ServiceResult> AddAsync(InfoObject model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      _context.InfoObjects.Add(model);

      return _context.SaveChangesSafeAsync(nameof(AddAsync));
    }

    public async Task<ServiceResult> UpdateAsync(InfoObject model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      if (await _context.InfoObjects.AnyAsync(x => x.Id == model.Id))
      {
        _context.InfoObjects.Update(model);
        return await _context.SaveChangesSafeAsync(nameof(UpdateAsync));
      }
      else
      {
        return ServiceResultFactory.NotFound;
      }
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var infoObject = await _context.InfoObjects.FirstOrDefaultAsync(x => x.Id == id);
      if (infoObject == null)
      {
        return ServiceResultFactory.Success;
      }

      _context.InfoObjects.Remove(infoObject);

      return await _context.SaveChangesSafeAsync(nameof(DeleteAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}