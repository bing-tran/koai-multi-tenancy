using System;
using System.Threading.Tasks;
using Koai.MultiTenancy.Abstractions;

namespace Koai.MultiTenancy.Strategies
{
    public class DelegateStrategy<TKey> : IMultiTenantStrategy<TKey>
    {
        private readonly Func<object, Task<TKey>> _doStrategy;

        public DelegateStrategy(Func<object, Task<TKey>> doStrategy)
        {
            _doStrategy = doStrategy;
        }

        public async Task<TKey> GetIdentifierAsync(object context)
        {
            var identifier = await _doStrategy(context);
            return await Task.FromResult(identifier);
        }
    }
}
