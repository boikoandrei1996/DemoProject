using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DemoProject.UnitTest.Infrastructure
{
  public static class MockDbSetExtensions
  {
    public static Mock<DbSet<TEntity>> SetupData<TEntity>(this Mock<DbSet<TEntity>> mock, IList<TEntity> list) 
      where TEntity : class
    {
      var data = list.AsQueryable();

      return MockDbSetExtensions.SetupData(mock, data);
    }

    private static Mock<DbSet<TEntity>> SetupData<TEntity>(this Mock<DbSet<TEntity>> mock, IQueryable<TEntity> data)
      where TEntity : class
    {
      mock.As<IQueryable<TEntity>>().Setup(x => x.Provider).Returns(data.Provider);
      mock.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(data.Expression);
      mock.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
      mock.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

      return mock;
    }
  }
}
