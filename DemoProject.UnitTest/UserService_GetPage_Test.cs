using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.BLL.Services;
using DemoProject.DAL;
using DemoProject.DAL.Models;
using DemoProject.UnitTest.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace DemoProject.UnitTest
{
  public class UserService_GetPage_Test
  {
    private readonly AppUser _user1;
    private readonly AppUser _user2;
    private readonly AppUser _user3;
    private readonly IList<AppUser> _data;
    private readonly Mock<DbSet<AppUser>> _dbSet;
    private readonly Mock<IDbContext> _context;

    public UserService_GetPage_Test()
    {
      _user1 = new AppUser
      {
        Username = Constants.USERNAME_VALID
      };

      _user2 = new AppUser
      {
        Username = "user2"
      };

      _user3 = new AppUser
      {
        Username = "user3"
      };

      _data = new List<AppUser> { _user1, _user2, _user3 };

      _dbSet = _data.AsDbSetMock();

      _context = new Mock<IDbContext>();
      _context.Setup(x => x.Users).Returns(_dbSet.Object);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetPage_PageIndex_NotPositive_Result_ArgumentException(int pageIndex)
    {
      // Arrange
      var userService = new UserService(_context.Object, null);

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentException>(
        nameof(pageIndex), 
        () => userService.GetPageAsync(pageIndex, Constants.PAGE_SIZE_VALID));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetPage_PageSize_NotPositive_Result_ArgumentException(int pageSize)
    {
      // Arrange
      var userService = new UserService(_context.Object, null);

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentException>(
        nameof(pageSize), 
        () => userService.GetPageAsync(Constants.PAGE_INDEX_VALID, pageSize));
    }

    [Fact]
    public async Task GetPage_Filter_Null_Result_ItemsCountSame()
    {
      // Arrange
      var userService = new UserService(_context.Object, null);

      // Act
      var page = await userService.GetPageAsync(1, _data.Count + 1, null);

      // Assert
      Assert.NotNull(page);
      Assert.NotNull(page.Records);
      Assert.Equal(_data.Count, page.Records.Count);
    }

    [Fact]
    public async Task GetPage_Filter_Valid_Result_ItemsCountLessThanBefore()
    {
      // Arrange
      var userService = new UserService(_context.Object, null);

      // Act
      var page = await userService.GetPageAsync(1, _data.Count + 1, x => x.Username == Constants.USERNAME_VALID);

      // Assert
      Assert.NotNull(page);
      Assert.NotNull(page.Records);
      Assert.True(page.Records.Count < _data.Count);
    }

    [Fact]
    public async Task GetPage_Filter_Invalid_Result_ItemsCount0()
    {
      // Arrange
      var userService = new UserService(_context.Object, null);

      // Act
      var page = await userService.GetPageAsync(1, _data.Count + 1, x => x.Username == Constants.USERNAME_INVALID);

      // Assert
      Assert.NotNull(page);
      Assert.NotNull(page.Records);
      Assert.Empty(page.Records);
    }
  }
}
