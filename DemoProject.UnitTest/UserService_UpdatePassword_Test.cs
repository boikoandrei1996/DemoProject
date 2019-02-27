using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.BLL.Services;
using DemoProject.DAL;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.Shared.Interfaces;
using DemoProject.UnitTest.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace DemoProject.UnitTest
{
  public class UserService_UpdatePassword_Test
  {
    private readonly AppUser _user;
    private readonly IList<AppUser> _data;
    private readonly Mock<DbSet<AppUser>> _dbSet;
    private readonly Mock<IDbContext> _context;
    private readonly Mock<IPasswordManager> _passwordManager;

    public UserService_UpdatePassword_Test()
    {
      _user = new AppUser
      {
        Id = Guid.Parse("92238225-466B-4DB2-9B75-B73BFD59ED18"),
        LastModified = DateTime.UtcNow
      };

      _data = new List<AppUser> { _user };

      _dbSet = _data.AsDbSetMock();

      _context = new Mock<IDbContext>();
      _context.Setup(x => x.Users).Returns(_dbSet.Object);

      _passwordManager = new Mock<IPasswordManager>();
      _passwordManager
        .Setup(x => x.CreatePasswordHash(It.IsAny<string>()))
        .Returns((It.IsAny<byte[]>(), It.IsAny<byte[]>()));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task UpdatePassword_NewPassword_NullOrEmpty_Result_ArgumentException(string newPassword)
    {
      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentException>(
        nameof(newPassword),
        () => userService.UpdatePasswordAsync(Guid.Empty, newPassword));
    }

    [Fact]
    public async Task UpdatePassword_Id_Invalid_Result_BadRequest()
    {
      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);

      // Act
      var result = await userService.UpdatePasswordAsync(Guid.Empty, Constants.PASSWORD);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(ServiceResultKey.BadRequest, result.Key);
      Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task UpdatePassword_Params_Valid_Result_EntityUpdated()
    {
      _context
        .Setup(x => x.SaveAsync(It.IsAny<string>(), null))
        .Returns(Task.FromResult(ServiceResultFactory.EntityUpdatedResult(null)));

      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);

      // Act
      var result = await userService.UpdatePasswordAsync(_data.First().Id, Constants.PASSWORD);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(ServiceResultKey.ModelUpdated, result.Key);
      Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task UpdatePassword_Params_Valid_Result_BadRequest()
    {
      _context
        .Setup(x => x.SaveAsync(It.IsAny<string>(), null))
        .Returns(Task.FromResult(ServiceResultFactory.BadRequestResult(It.IsAny<string>(), It.IsAny<string>())));

      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);

      // Act
      var result = await userService.UpdatePasswordAsync(_data.First().Id, Constants.PASSWORD);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(ServiceResultKey.BadRequest, result.Key);
      Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task Add_Model_Valid_Result_InternalServerError()
    {
      _context
        .Setup(x => x.SaveAsync(It.IsAny<string>(), null))
        .Returns(Task.FromResult(ServiceResultFactory.InternalServerErrorResult(It.IsAny<string>())));

      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);

      // Act
      var result = await userService.UpdatePasswordAsync(_data.First().Id, Constants.PASSWORD);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(ServiceResultKey.InternalServerError, result.Key);
      Assert.NotEmpty(result.Errors);
    }
  }
}
