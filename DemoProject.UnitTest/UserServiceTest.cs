using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
  public class UserServiceTest
  {
    private readonly IList<AppUser> _data;
    private readonly Mock<DbSet<AppUser>> _dbSet;
    private readonly Mock<IDbContext> _context;
    private readonly Mock<IPasswordManager> _passwordManager;

    public UserServiceTest()
    {
      _data = new List<AppUser>();
      _dbSet = new Mock<DbSet<AppUser>>();
      _context = new Mock<IDbContext>();
      _passwordManager = new Mock<IPasswordManager>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("1")]
    public async Task Authenticate_UsernameNullOrEmpty_ArgumentException(string username)
    {
      _dbSet.SetupData(_data);
      _context.Setup(x => x.Users).Returns(_dbSet.Object);
      _passwordManager.Setup(x => x.VerifyPassword(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(true);

      // Arrange
      var userService = new UserService(_context.Object, _passwordManager.Object);

      // Act
      var task = userService.AuthenticateAsync(username, Constants.PASSWORD);

      // Assert
      await Assert.ThrowsAsync<ArgumentException>("username", () => task);
    }
  }
}
