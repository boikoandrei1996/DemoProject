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
  public class UserService_Exist_Test
  {
    private readonly AppUser _user;
    private readonly IList<AppUser> _data;
    private readonly Mock<DbSet<AppUser>> _dbSet;
    private readonly Mock<IDbContext> _context;

    public UserService_Exist_Test()
    {
      _user = new AppUser
      {
        Username = Constants.USERNAME_VALID
      };

      _data = new List<AppUser> { _user };

      _dbSet = _data.AsDbSetMock();

      _context = new Mock<IDbContext>();
      _context.Setup(x => x.Users).Returns(_dbSet.Object);
    }

    [Fact]
    public async Task Exist_Filter_Null_Result_ArgumentNullException()
    {
      // Arrange
      var userService = new UserService(_context.Object, null);

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentNullException>(
        "filter", 
        () => userService.ExistAsync(null));
    }

    [Fact]
    public async Task Exist_Filter_Valid_Result_True()
    {
      // Arrange
      var userService = new UserService(_context.Object, null);

      // Act
      var exist = await userService.ExistAsync(x => x.Username == Constants.USERNAME_VALID);

      // Assert
      Assert.True(exist);
    }

    [Fact]
    public async Task Exist_Filter_Invalid_Result_False()
    {
      // Arrange
      var userService = new UserService(_context.Object, null);

      // Act
      var exist = await userService.ExistAsync(x => x.Username == Constants.USERNAME_INVALID);

      // Assert
      Assert.False(exist);
    }
  }
}
