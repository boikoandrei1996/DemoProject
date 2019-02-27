using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.BLL.Services;
using DemoProject.DAL;
using DemoProject.DAL.Models;
using DemoProject.Shared.Interfaces;
using DemoProject.UnitTest.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace DemoProject.UnitTest
{
  public class UserService_Authenticate_Test
  {
    private readonly AppUser _user;
    private readonly IList<AppUser> _data;
    private readonly Mock<DbSet<AppUser>> _dbSet;
    private readonly Mock<IDbContext> _context;
    private readonly Mock<IPasswordManager> _passwordManager;

    public UserService_Authenticate_Test()
    {
      _user = new AppUser
      {
        Username = Constants.USERNAME_VALID
      };

      _data = new List<AppUser> { _user };

      _dbSet = _data.AsDbSetMock();

      _context = new Mock<IDbContext>();
      _context.Setup(x => x.Users).Returns(_dbSet.Object);

      _passwordManager = new Mock<IPasswordManager>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Authenticate_Username_NullOrEmpty_Result_ArgumentException(string username)
    {
      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentException>(
        nameof(username), 
        () => userService.AuthenticateAsync(username, Constants.PASSWORD));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Authenticate_Password_NullOrEmpty_Result_ArgumentException(string password)
    {
      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentException>(
        nameof(password), 
        () => userService.AuthenticateAsync(Constants.USERNAME_VALID, password));
    }

    [Fact]
    public async Task Authenticate_UserFindBy_ValidUsername_Result_NotNull()
    {
      _passwordManager
        .Setup(x => x.VerifyPassword(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>()))
        .Returns(true);

      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);

      // Act
      var result = await userService.AuthenticateAsync(Constants.USERNAME_VALID, Constants.PASSWORD);

      // Assert
      Assert.NotNull(result);
    }

    [Fact]
    public async Task Authenticate_UserFindBy_InvalidUsername_Result_Null()
    {
      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);

      // Act
      var result = await userService.AuthenticateAsync(Constants.USERNAME_INVALID, Constants.PASSWORD);

      // Assert
      Assert.Null(result);
    }

    [Fact]
    public async Task Authenticate_VerifyPassword_Success_Result_NotNull()
    {
      _passwordManager
        .Setup(x => x.VerifyPassword(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>()))
        .Returns(true);

      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);

      // Act
      var result = await userService.AuthenticateAsync(Constants.USERNAME_VALID, Constants.PASSWORD);

      // Assert
      Assert.NotNull(result);
    }

    [Fact]
    public async Task Authenticate_VerifyPassword_Fail_Result_Null()
    {
      _passwordManager
        .Setup(x => x.VerifyPassword(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>()))
        .Returns(false);

      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);

      // Act
      var result = await userService.AuthenticateAsync(Constants.USERNAME_VALID, Constants.PASSWORD);

      // Assert
      Assert.Null(result);
    }
  }
}
