using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.BLL.Services;
using DemoProject.DAL;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.UnitTest.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace DemoProject.UnitTest
{
  public class UserService_Delete_Test
  {
    private readonly AppUser _user;
    private readonly IList<AppUser> _data;
    private readonly Mock<DbSet<AppUser>> _dbSet;
    private readonly Mock<IDbContext> _context;

    public UserService_Delete_Test()
    {
      _user = new AppUser
      {
        Id = Guid.Parse("92238225-466B-4DB2-9B75-B73BFD59ED18"),
        Username = Constants.USERNAME_VALID
      };

      _data = new List<AppUser> { _user };

      _dbSet = _data.AsDbSetMock();

      _context = new Mock<IDbContext>();
      _context.Setup(x => x.Users).Returns(_dbSet.Object);
      _context.Setup(x => x.Set<AppUser>()).Returns(_dbSet.Object);
    }

    [Fact]
    public async Task Delete_Id_Invalid_Result_Success()
    {
      // Arrange
      var userService = new UserService(_context.Object, null);

      // Act
      var result = await userService.DeleteAsync(Guid.Empty);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(ServiceResultKey.NotFound, result.Key);
    }

    [Fact]
    public async Task Delete_Id_Valid_Result_Success()
    {
      _context
        .Setup(x => x.SaveAsync(It.IsAny<string>()))
        .Returns(Task.FromResult(ServiceResultFactory.Success));

      // Arrange
      var userService = new UserService(_context.Object, null);

      // Act
      var result = await userService.DeleteAsync(_data.First().Id);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(ServiceResultKey.Success, result.Key);
      Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task Delete_Id_Valid_Result_BadRequest()
    {
      _context
        .Setup(x => x.SaveAsync(It.IsAny<string>()))
        .Returns(Task.FromResult(ServiceResultFactory.BadRequestResult(It.IsAny<string>(), It.IsAny<string>())));

      // Arrange
      var userService = new UserService(_context.Object, null);

      // Act
      var result = await userService.DeleteAsync(_data.First().Id);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(ServiceResultKey.BadRequest, result.Key);
      Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task Delete_Id_Valid_Result_InternalServerError()
    {
      _context
        .Setup(x => x.SaveAsync(It.IsAny<string>()))
        .Returns(Task.FromResult(ServiceResultFactory.InternalServerErrorResult(It.IsAny<string>())));

      // Arrange
      var userService = new UserService(_context.Object, null);

      // Act
      var result = await userService.DeleteAsync(_data.First().Id);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(ServiceResultKey.InternalServerError, result.Key);
      Assert.NotEmpty(result.Errors);
    }
  }
}
