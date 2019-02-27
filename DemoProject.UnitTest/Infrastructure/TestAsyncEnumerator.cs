using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DemoProject.UnitTest.Infrastructure
{
  public sealed class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
  {
    private readonly IEnumerator<T> _inner;

    internal TestAsyncEnumerator(IEnumerator<T> inner)
    {
      _inner = inner;
    }

    public void Dispose()
    {
      _inner.Dispose();
    }

    public T Current => _inner.Current;

    public Task<bool> MoveNext(CancellationToken cancellationToken)
    {
      return Task.FromResult(_inner.MoveNext());
    }
  }
}
