using System;
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
        return ServiceResultFactory.BadRequestResult(nameof(model.ContentGroupId), $"ContentGroup not found with id: '{model.ContentGroupId}'.");
      }

      _context.InfoObjects.Add(model);
      _context.History.Add(ChangeHistory.Create(TableName.InfoObject, ActionType.Add));

      var result = await _context.SaveAsync(nameof(AddAsync));
      result.SetModelIfSuccess(model);

      return result;
    }

    public async Task<ServiceResult> UpdateAsync(InfoObject model)
    {
      Check.NotNull(model, nameof(model));

      var infoObject = await _context.InfoObjects.FirstOrDefaultAsync(x => x.Id == model.Id);
      if (infoObject == null)
      {
        return ServiceResultFactory.NotFound;
      }

      var changed = false;

      // update contentGroup
      if (Utility.IsModified(infoObject.ContentGroupId, model.ContentGroupId))
      {
        var contentGroupExist = await _context.ContentGroups.AnyAsync(x => x.Id == model.ContentGroupId);
        if (contentGroupExist == false)
        {
          return ServiceResultFactory.BadRequestResult(nameof(model.ContentGroupId), $"ContentGroup not found with id: '{model.ContentGroupId}'.");
        }

        infoObject.ContentGroupId = model.ContentGroupId;
        changed = true;
      }

      // update content
      if (Utility.IsModified(infoObject.Content, model.Content))
      {
        infoObject.Content = model.Content;
        changed = true;
      }

      // update type
      if (model.Type != infoObject.Type)
      {
        infoObject.Type = model.Type;
        changed = true;
      }

      // update subOrder
      if (Utility.IsModified(infoObject.SubOrder, model.SubOrder))
      {
        infoObject.SubOrder = model.SubOrder;
        changed = true;
      }

      if (changed == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(UpdateAsync), "Nothing to update.");
      }

      _context.InfoObjects.Update(infoObject);
      _context.History.Add(ChangeHistory.Create(TableName.InfoObject, ActionType.Modify));

      var result = await _context.SaveAsync(nameof(UpdateAsync));
      result.SetModelIfSuccess(infoObject);

      return result;
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var model = await _context.InfoObjects.FirstOrDefaultAsync(x => x.Id == id);
      if (model == null)
      {
        return ServiceResultFactory.NotFound;
      }

      _context.InfoObjects.Remove(model);
      _context.History.Add(ChangeHistory.Create(TableName.InfoObject, ActionType.Delete));

      return await _context.SaveAsync(nameof(DeleteAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
