using System;
using System.Threading.Tasks;
using Koai.MultiTenancy.Abstractions;

namespace Koai.MultiTenancy.Strategies
{
    public class StaticStrategy<TKey> : IMultiTenantStrategy<TKey>
    {
        private readonly TKey _identifier;

        public StaticStrategy(TKey identifier)
        {
            _identifier = identifier;
        }

        public Task<TKey> GetIdentifierAsync(object context)
        {
            return Task.FromResult<TKey>(_identifier);
        }
    }
}
