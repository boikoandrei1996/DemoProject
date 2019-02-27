using System;
using System.Collections.Generic;
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
  public class UserService_Add_Test
  {
    private readonly AppUser _user;
    private readonly IList<AppUser> _data;
    private readonly Mock<DbSet<AppUser>> _dbSet;
    private readonly Mock<IDbContext> _context;
    private readonly Mock<IPasswordManager> _passwordManager;

    public UserService_Add_Test()
    {
      _user = new AppUser
      {
        Id = Guid.Parse("92238225-466B-4DB2-9B75-B73BFD59ED18"),
        Username = Constants.USERNAME_VALID,
        Email = Constants.EMAIL_VALID
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

    [Fact]
    public async Task Add_Model_Null_Result_ArgumentNullException()
    {
      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentNullException>(
        "model",
        () => userService.AddAsync(null, Constants.PASSWORD));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Add_Password_NullOrEmpty_Result_ArgumentException(string password)
    {
      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentException>(
        nameof(password),
        () => userService.AddAsync(new AppUser(), password));
    }

    [Fact]
    public async Task Add_ModelUsername_NonUnique_Result_BadRequest()
    {
      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);
      var user = new AppUser
      {
        Username = Constants.USERNAME_VALID,
      };

      // Act
      var result = await userService.AddAsync(user, Constants.PASSWORD);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(ServiceResultKey.BadRequest, result.Key);
      Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task Add_ModelEmail_NonUnique_Result_BadRequest()
    {
      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);
      var user = new AppUser
      {
        Username = "test_username",
        Email = Constants.EMAIL_VALID
      };

      // Act
      var result = await userService.AddAsync(user, Constants.PASSWORD);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(ServiceResultKey.BadRequest, result.Key);
      Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task Add_ModelRole_Invalid_Result_BadRequest()
    {
      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);
      var user = new AppUser
      {
        Username = "test_username",
        Email = "test_email@gmail.com",
        Role = "test-" + Role.Admin
      };

      // Act
      var result = await userService.AddAsync(user, Constants.PASSWORD);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(ServiceResultKey.BadRequest, result.Key);
      Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task Add_ModelRole_Normalized_Result_EntityCreated()
    {
      _context
        .Setup(x => x.SaveAsync(It.IsAny<string>(), Guid.Empty))
        .Returns(Task.FromResult(ServiceResultFactory.EntityCreatedResult(Guid.Empty)));

      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);
      var user = new AppUser
      {
        Username = "test_username",
        Email = "test_email@gmail.com",
        Role = Role.Admin
      };

      // Act
      var result = await userService.AddAsync(user, Constants.PASSWORD);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(ServiceResultKey.ModelCreated, result.Key);
      Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task Add_ModelRole_NotNormalized_Result_EntityCreated()
    {
      _context
        .Setup(x => x.SaveAsync(It.IsAny<string>(), Guid.Empty))
        .Returns(Task.FromResult(ServiceResultFactory.EntityCreatedResult(Guid.Empty)));

      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);
      var user = new AppUser
      {
        Username = "test_username",
        Email = "test_email@gmail.com",
        Role = Role.Admin.ToUpper()
      };

      // Act
      var result = await userService.AddAsync(user, Constants.PASSWORD);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(ServiceResultKey.ModelCreated, result.Key);
      Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task Add_Model_Valid_Result_BadRequest()
    {
      _context
        .Setup(x => x.SaveAsync(It.IsAny<string>(), Guid.Empty))
        .Returns(Task.FromResult(ServiceResultFactory.BadRequestResult(It.IsAny<string>(), It.IsAny<string>())));

      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);
      var user = new AppUser
      {
        Username = "test_username",
        Email = "test_email@gmail.com",
        Role = Role.Admin
      };

      // Act
      var result = await userService.AddAsync(user, Constants.PASSWORD);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(ServiceResultKey.BadRequest, result.Key);
      Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task Add_Model_Valid_Result_InternalServerError()
    {
      _context
        .Setup(x => x.SaveAsync(It.IsAny<string>(), Guid.Empty))
        .Returns(Task.FromResult(ServiceResultFactory.InternalServerErrorResult(It.IsAny<string>())));

      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);
      var user = new AppUser
      {
        Username = "test_username",
        Email = "test_email@gmail.com",
        Role = Role.Admin
      };

      // Act
      var result = await userService.AddAsync(user, Constants.PASSWORD);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(ServiceResultKey.InternalServerError, result.Key);
      Assert.NotEmpty(result.Errors);
    }
  }
}
