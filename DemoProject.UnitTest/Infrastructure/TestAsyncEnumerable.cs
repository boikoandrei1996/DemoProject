using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DemoProject.UnitTest.Infrastructure
{
  public sealed class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
  {
    public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable) { }

    public TestAsyncEnumerable(Expression expression) : base(expression) { }

    public IAsyncEnumerator<T> GetEnumerator()
    {
      return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }

    public IQueryProvider Provider => new TestAsyncQueryProvider<T>(this);

    IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(this);
  }
}
