﻿using System.Threading.Tasks;
using DemoProject.DLL.Models;
using DemoProject.DLL.Models.Pages;
using DemoProject.Shared.Interfaces;

namespace DemoProject.DLL.Interfaces
{
  public interface IMenuItemService : IChangeableService<MenuItem>, IReadableService<MenuItem, MenuItemPage>
  {
    Task<ChangeHistory> GetHistoryRecordAsync();
  }
}
