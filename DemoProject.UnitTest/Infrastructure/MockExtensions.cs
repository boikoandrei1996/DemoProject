using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DemoProject.UnitTest.Infrastructure
{
  public static class MockExtensions
  {
    public static Mock<DbSet<TEntity>> AsDbSetMock<TEntity>(this IEnumerable<TEntity> entities) 
      where TEntity : class
    {
      var query = entities.AsQueryable();

      var asyncEnumerable = new TestAsyncEnumerable<TEntity>(query);

      var dbSetMock = new Mock<DbSet<TEntity>>();

      dbSetMock.As<IQueryable<TEntity>>().Setup(x => x.Expression).Returns(query.Expression);
      dbSetMock.As<IQueryable<TEntity>>().Setup(x => x.ElementType).Returns(query.ElementType);
      dbSetMock.As<IQueryable<TEntity>>().Setup(x => x.Provider).Returns(asyncEnumerable.Provider);
      dbSetMock.As<IQueryable<TEntity>>().Setup(x => x.GetEnumerator()).Returns(query.GetEnumerator());
      dbSetMock.As<IAsyncEnumerable<TEntity>>().Setup(x => x.GetEnumerator()).Returns(asyncEnumerable.GetEnumerator());

      return dbSetMock;
    }
  }
}
