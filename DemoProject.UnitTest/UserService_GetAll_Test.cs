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
  public class UserService_GetAll_Test
  {
    private readonly AppUser _user1;
    private readonly AppUser _user2;
    private readonly AppUser _user3;
    private readonly IList<AppUser> _data;
    private readonly Mock<DbSet<AppUser>> _dbSet;
    private readonly Mock<IDbContext> _context;

    public UserService_GetAll_Test()
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

    [Fact]
    public async Task GetAll_Filter_Null_Result_CountSame()
    {
      // Arrange
      var userService = new UserService(_context.Object, null);

      // Act
      var users = await userService.GetListAsync(null);

      // Assert
      Assert.NotNull(users);
      Assert.Equal(_data.Count, users.Count);
    }

    [Fact]
    public async Task GetAll_Filter_Valid_Result_CountLessThanBefore()
    {
      // Arrange
      var userService = new UserService(_context.Object, null);

      // Act
      var users = await userService.GetListAsync(x => x.Username == Constants.USERNAME_VALID);

      // Assert
      Assert.NotNull(users);
      Assert.True(users.Count < _data.Count);
    }

    [Fact]
    public async Task GetAll_Filter_Invalid_Result_Count0()
    {
      // Arrange
      var userService = new UserService(_context.Object, null);

      // Act
      var users = await userService.GetListAsync(x => x.Username == Constants.USERNAME_INVALID);

      // Assert
      Assert.NotNull(users);
      Assert.Empty(users);
    }
  }
}
