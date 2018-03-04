using Microsoft.Extensions.ObjectPool;
using System;

namespace ThoughtHaven.AspNetCore.Mvc.Fakes
{
    public class FakeObjectPoolProvider : ObjectPoolProvider
    {
        public FakeObjectPoolProvider() : base() { }

        public override ObjectPool<T> Create<T>(IPooledObjectPolicy<T> policy) =>
            throw new NotImplementedException();
    }
}